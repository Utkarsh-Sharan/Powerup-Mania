using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTr01Enemy : MonoBehaviour
{
    private float _moveSpeed = 2f;
    private static int _enemyID = 2;

    public static bool playerInDetectionZone;

    private void Start()
    {
        if (LevelManager.Instance.IsEnemyDestroyed(_enemyID))
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (playerInDetectionZone && PowerupManager.IsInvisibilityPowerupActivated)
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

    public static int GetEnemyID()
    {
        return _enemyID;
    }
}
