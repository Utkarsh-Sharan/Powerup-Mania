using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Tr01Enemy.playerInDetectionZone = true;
            BlueTr01Enemy.playerInDetectionZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Tr01Enemy.playerInDetectionZone = false;
            BlueTr01Enemy.playerInDetectionZone = false;
        }
    }
}
