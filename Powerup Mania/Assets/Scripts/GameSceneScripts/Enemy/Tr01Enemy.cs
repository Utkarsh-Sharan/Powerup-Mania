using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tr01Enemy : MonoBehaviour
{
    private float _moveSpeed = 5f;
    private static int _enemyID = 1;

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
        if (playerInDetectionZone && CheckPlayerInvisibilityAndLifeStatus())
        {
            Transform playerTransform = PlayerTransformManager.Instance.playerTransform;

            float distance = Vector2.Distance(this.transform.position, playerTransform.position);
            if (distance < 7f)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, _moveSpeed * Time.deltaTime);
            }
        }
    }

    private bool CheckPlayerInvisibilityAndLifeStatus()
    {
        return (!PowerupManager.IsInvisibilityPowerupActivated && PlayerController.playerLifeStatus == PlayerLifeStatus.ALIVE);
    }

    public static int GetEnemyID()
    {
        return _enemyID;
    }
}
