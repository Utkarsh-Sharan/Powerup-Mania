using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    private float _followSpeed = 5f; //smooth follow speed

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(_playerTransform.position.x, _playerTransform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);   //lerp for smooth camera movement
    }
}
