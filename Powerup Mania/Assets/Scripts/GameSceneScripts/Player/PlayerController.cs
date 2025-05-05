using System.Collections;
using UnityEngine;

public class PlayerController
{
    public static PlayerLifeStatus playerLifeStatus { get; set; }
    public PlayerLifeStatus PlayerLifeStatus { get; set; }
   
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;

    private PlayerView _playerView;

    private float _fireRate;
    private float _playerSpeed;
    private float _playerRotationSpeed;
    private float _countdownDuration;

    private float _fireTime;

    private float _timeLeft;
    private Coroutine _deathCountdownCoroutine;

    public PlayerController(PlayerScriptableObject playerSO, PlayerView playerView)
    {
        _fireRate = playerSO.FireRate;
        _playerSpeed = playerSO.PlayerSpeed;
        _playerRotationSpeed = playerSO.PlayerRotationSpeed;
        _countdownDuration = playerSO.CountdownDuration;

        _playerView = Object.Instantiate(playerView);
        _playerView.Initialize(this);
    }

    public void UpdatePlayer()
    {
        HandlePlayerLifestatus();

        if(playerLifeStatus == PlayerLifeStatus.ALIVE)
        {
            HandleMovement();
            HandleRotation();

            if (Input.GetMouseButtonDown(0) && Time.time > _fireTime)
            {
                Shoot();
                _fireTime = Time.time + _fireRate;
            }
        }
    }

    private void HandlePlayerLifestatus()
    {
        if(playerLifeStatus == PlayerLifeStatus.ALIVE && !PowerupManager.IsInvisibilityPowerupActivated)
        {
            _playerView.HandlePlayerAlpha(true);

            // If player becomes ALIVE, stop the countdown and reset the timer
            if (_deathCountdownCoroutine != null)
            {
                _playerView.StopDeathCountdownRoutine();
                SoundManager.Instance.PlayMusic(Sounds.BACKGROUND_MUSIC);
            }
        }
        else if(playerLifeStatus == PlayerLifeStatus.DEAD)
        {
            _playerView.HandlePlayerAlpha(false);

            // Start the countdown if it's not already running
            if (_deathCountdownCoroutine == null)
            {
                SoundManager.Instance.PlayMusic(Sounds.HEART_BEAT);
                _deathCountdownCoroutine = _playerView.StartDeathCountdownRoutine(_countdownDuration);
            }
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        _playerView.GetPlayerTransform().position += direction * _playerSpeed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - _playerView.GetPlayerTransform().position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        _playerView.GetPlayerTransform().rotation = Quaternion.Slerp(_playerView.GetPlayerTransform().rotation, targetRotation, _playerRotationSpeed * Time.deltaTime);
    }

    private void Shoot() => Object.Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
}