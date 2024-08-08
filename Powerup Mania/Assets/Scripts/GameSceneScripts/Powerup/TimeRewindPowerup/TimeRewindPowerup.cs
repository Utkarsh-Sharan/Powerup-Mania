using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            PowerupManager.IsTimeRewindActivated = true;

            Destroy(this.gameObject);
        }
    }
}
