using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tr01Enemy : MonoBehaviour
{
    private float _moveSpeed = 2f;
    public static bool playerInDetectionZone;

    private void Update()
    {
        if (playerInDetectionZone)
        {
            Transform playerTransform = PlayerTransformManager.Instance.playerTransform;

            float distance = Vector2.Distance(this.transform.position, playerTransform.position);
            if (distance < 5f)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, _moveSpeed * Time.deltaTime);
            }
        }
    }
}
