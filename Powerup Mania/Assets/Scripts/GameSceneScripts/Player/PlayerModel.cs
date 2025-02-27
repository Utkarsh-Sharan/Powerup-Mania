using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    private SpriteRenderer _playerSpriteRenderer;

    private float _playerSpeed = 10f;
    private float _playerRotationSpeed = 5f;

    private float _shootForce = 5f;
    private float _fireRate = 0.4f;
    private float _fireTime;

    public PlayerModel(SpriteRenderer playerSpriteRenderer)
    {
        _playerSpriteRenderer = playerSpriteRenderer;
    }

    public Vector3 HandleMovement(float horizontalInput, float verticalInput)
    {
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        return direction * _playerSpeed * Time.deltaTime;
    }

    public Quaternion HandleRotation(Transform playerTransform, Camera mainCamera)
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - playerTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        return Quaternion.Slerp(playerTransform.rotation, targetRotation, _playerRotationSpeed * Time.deltaTime);
    }

    public void HandlePlayerAlpha(bool isPlayerAlive)
    {
        if(isPlayerAlive)
        {
            Color playerColor = _playerSpriteRenderer.color;
            playerColor.a = 1;
            _playerSpriteRenderer.color = playerColor;
        }
        else
        {
            Color playerColor = _playerSpriteRenderer.color;
            playerColor.a = 0;
            _playerSpriteRenderer.color = playerColor;
        }
    }
}
