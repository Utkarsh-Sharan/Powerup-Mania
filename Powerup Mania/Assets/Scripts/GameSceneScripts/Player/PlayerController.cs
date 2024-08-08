using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPoint;

    private float _playerSpeed = 10f;
    private float _playerRotationSpeed = 5f;
    private float _horizontalInput;
    private float _verticalInput;

    private float _shootForce = 5f;
    private float _fireRate = 0.4f;
    private float _fireTime;

    private float _rewindDuration = 10f; // Duration in seconds to rewind

    private void Start()
    {
        transform.position = Vector3.zero;
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

        if(Input.GetKeyDown(KeyCode.T) && PowerupManager.IsTimeRewindActivated)
        {
            StartCoroutine(RewindCoroutine());
        }

        if (PowerupManager.IsMagnetPowerupActivated)
        {
            MoveTowardsSpikes();
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

    private IEnumerator RewindCoroutine()
    {
        // Wait for a short moment to ensure all actions are processed
        yield return new WaitForSeconds(0.1f);

        // Rewind the game state
        float startTime = Time.time;
        while (Time.time - startTime < _rewindDuration)
        {
            TimeManager.Instance.RewindState();
            yield return new WaitForSeconds(0.1f); // Adjust the delay as needed
        }
    }

    private void MoveTowardsSpikes()
    {
        Spikes spikes = GetComponent<Spikes>();

        if(spikes != null)
        {
            float distance = Vector2.Distance(spikes.spikeTransform.position, this.transform.position);

            if (distance < 5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, spikes.spikeTransform.position, 5 * Time.deltaTime);
            }
        }
        else
        {
            Debug.LogError("Spikes not found");
        }
    }
}
