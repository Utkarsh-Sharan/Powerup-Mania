using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerLifeStatus playerLifeStatus;
   
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;

    private PlayerModel _playerModel;

    private float _horizontalInput;
    private float _verticalInput;

    private float _fireRate = 0.4f;
    private float _fireTime;

    private float _countdownDuration = 9f;
    private float _timeLeft;
    private Coroutine _deathCountdownCoroutine;

    private void Start()
    {
        _playerModel = new PlayerModel(_playerSpriteRenderer);

        if (LevelManager.Instance.playerCameBackFromPortalLevel)
        {
            transform.position = LevelManager.playerLastPosition;
            LevelManager.Instance.playerCameBackFromPortalLevel = false;
        }
        else
        {
            transform.position = Vector3.zero;
        }
    }

    private void Update()
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
            _playerModel.HandlePlayerAlpha(true);

            // If player becomes ALIVE, stop the countdown and reset the timer
            if (_deathCountdownCoroutine != null)
            {
                StopCoroutine(_deathCountdownCoroutine);
                _deathCountdownCoroutine = null;

                SoundManager.Instance.PlayMusic(Sounds.BACKGROUND_MUSIC);
            }
        }
        else if(playerLifeStatus == PlayerLifeStatus.DEAD)
        {
            _playerModel.HandlePlayerAlpha(false);

            // Start the countdown if it's not already running
            if (_deathCountdownCoroutine == null)
            {
                SoundManager.Instance.PlayMusic(Sounds.HEART_BEAT);
                _deathCountdownCoroutine = StartCoroutine(DeathCountdown());
            }
        }
    }

    private IEnumerator DeathCountdown()
    {
        _timeLeft = _countdownDuration;

        while (_timeLeft > 0)
        {
            yield return new WaitForSeconds(1f);
            _timeLeft -= 1f;

            // If player comes back to life during countdown, stop the Coroutine
            if (playerLifeStatus == PlayerLifeStatus.ALIVE)
            {
                _deathCountdownCoroutine = null;
                yield break;
            }
        }

        GameManager.Instance.LoadGameOverScene(GameOverType.TIME_REWIND_GAME_OVER);
        playerLifeStatus = PlayerLifeStatus.ALIVE;
    }

    private void HandleMovement()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        
        transform.position += _playerModel.HandleMovement(_horizontalInput, _verticalInput);
    }

    private void HandleRotation()
    {
        transform.rotation = _playerModel.HandleRotation(this.transform, _mainCamera);
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
        SoundManager.Instance.Play(Sounds.PLAYER_SHOT_LASER);
    }
}