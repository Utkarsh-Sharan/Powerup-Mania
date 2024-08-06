using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _playerSpeed = 10f;
    private float _horizontalInput;
    private float _verticalInput;

    private void Start()
    {
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        
        Vector3 direction = new Vector3(_horizontalInput, _verticalInput, 0);

        transform.position += direction * _playerSpeed * Time.deltaTime;
    }
}
