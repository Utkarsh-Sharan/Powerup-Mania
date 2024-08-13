using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPoint;

    //player movement and input
    private float _playerSpeed = 10f;
    private float _playerRotationSpeed = 5f;
    private float _horizontalInput;
    private float _verticalInput;

    //player shoot
    private float _shootForce = 5f;
    private float _fireRate = 0.4f;
    private float _fireTime;

    //coroutine variable as i am calling coroutine in update
    private bool _isCoroutineActive;

    //time rewind powerup
    private float _rewindDuration = 10f; // Duration in seconds to rewind

    //magnet powerup
    private float _magnetRange = 5f;
    private float _magnetSpeed = 0.1f;

    //invisibility powerup
    private SpriteRenderer _playerSpriteRenderer;
    private float _invisibilityDuration = 5f;

    private void Start()
    {
        transform.position = Vector3.zero;

        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();

        if (Input.GetMouseButtonDown(0) && Time.time > _fireTime)
        {
            Shoot();
            _fireTime = Time.time + _fireRate;
        }

        if(!_isCoroutineActive && Input.GetKeyDown(KeyCode.T) && PowerupManager.IsTimeRewindActivated)
        {
            _isCoroutineActive = true;
            StartCoroutine(RewindRoutine());
        }

        if (PowerupManager.IsMagnetPowerupActivated)
        {
            MoveTowardsSpikes();
        }

        if (!_isCoroutineActive && PowerupManager.IsInvisibilityPowerupActivated)
        {
            _isCoroutineActive = true;
            Debug.Log("hey invisible");
            StartCoroutine(InvisibilityOverRoutine());
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

    private IEnumerator RewindRoutine()
    {
        // Waiting for a short moment to ensure all actions are processed
        yield return new WaitForSeconds(0.1f);

        // Rewinding the game state
        float startTime = Time.time;
        while (Time.time - startTime < _rewindDuration)
        {
            TimeManager.Instance.RewindState();
            yield return new WaitForSeconds(0.1f);
        }

        _isCoroutineActive = false;
    }

    private void MoveTowardsSpikes()
    {
        List<Transform> spikes = SpikeManager.Instance.GetSpikes();

        foreach (Transform spikeTransform in spikes)
        {
            float distance = Vector2.Distance(spikeTransform.position, transform.position);

            if (distance < _magnetRange)
            {
                transform.position = Vector2.MoveTowards(spikeTransform.position, transform.position, _magnetSpeed * Time.deltaTime);
                break;
            }
        }
    }

    private IEnumerator InvisibilityOverRoutine()
    {
        Color playerColor = _playerSpriteRenderer.color;
        playerColor.a = 0.3f;
        _playerSpriteRenderer.color = playerColor;

        yield return new WaitForSeconds(_invisibilityDuration);

        playerColor.a = 1f;
        _playerSpriteRenderer.color = playerColor;

        _isCoroutineActive = false;
        PowerupManager.IsInvisibilityPowerupActivated = false;
    }
}
