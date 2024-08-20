using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            playerColor.a = 255;
            _playerSpriteRenderer.color = playerColor;
        }
        else if(playerLifeStatus == PlayerLifeStatus.DEAD)
        {
            Color playerColor = _playerSpriteRenderer.color;
            playerColor.a = 0;
            _playerSpriteRenderer.color = playerColor;
        }
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