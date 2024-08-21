using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerLifeStatus playerLifeStatus;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;

    //player movement and input
    private float _playerSpeed = 10f;
    private float _playerRotationSpeed = 5f;
    private float _horizontalInput;
    private float _verticalInput;

    //player shoot
    private float _shootForce = 5f;
    private float _fireRate = 0.4f;
    private float _fireTime;

    //death countdown
    private Coroutine _deathCountdownCoroutine;
    private float _countdownDuration = 9f;
    private float _timeLeft;

    private void Start()
    {
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
            Color playerColor = _playerSpriteRenderer.color;
            playerColor.a = 1;
            _playerSpriteRenderer.color = playerColor;

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
            Color playerColor = _playerSpriteRenderer.color;
            playerColor.a = 0;
            _playerSpriteRenderer.color = playerColor;

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
        
        Vector3 direction = new Vector3(_horizontalInput, _verticalInput, 0);

        transform.position += direction * _playerSpeed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        // Getting mouse position in world coordinates
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // z-axis to be 0 for 2D

        // Calculating direction and target rotation
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;

        // Smoothly rotating towards the target
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _playerRotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);

        SoundManager.Instance.Play(Sounds.PLAYER_SHOT_LASER);
        PlayerBullet bulletComponent = bullet.GetComponent<PlayerBullet>();
        if (bulletComponent)
        {
            Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
            if (bulletRigidBody)
            {
                bulletRigidBody.velocity = _shootPoint.up * _shootForce;
            }
        }
    }
}

public enum PlayerLifeStatus
{
    ALIVE,
    DEAD
}