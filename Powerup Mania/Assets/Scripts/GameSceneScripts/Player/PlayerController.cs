using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private float _playerSpeed = 10f;
    private float _playerRotationSpeed = 5f;
    private float _horizontalInput;
    private float _verticalInput;

    private void Start()
    {
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
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
        // Get mouse position in world coordinates
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure z-axis is zero for 2D

        // Calculate direction and target rotation
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;

        // Smoothly rotate towards the target rotation
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _playerRotationSpeed * Time.deltaTime);
    }
}
