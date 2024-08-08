using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            PowerupManager.IsMagnetPowerupActivated = true;

            Destroy(this.gameObject);
        }
    }
}
