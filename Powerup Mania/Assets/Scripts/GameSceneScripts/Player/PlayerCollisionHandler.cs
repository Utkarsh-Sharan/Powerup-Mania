using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var powerup = other.GetComponent<Powerup>();

        if (powerup != null)
        {
            if (powerup is TimeRewindPowerup)
            {
                PowerupManager.IsTimeRewindActivated = true;
            }
            else if (powerup is MagnetPowerup)
            {
                PowerupManager.IsMagnetPowerupActivated = true;
            }
            else if (powerup is PortalPowerup)
            {
                PowerupManager.IsPortalPowerupActivated = true;
            }
            else if (powerup is InvisibilityPowerup)
            {
                PowerupManager.IsInvisibilityPowerupActivated = true;
            }
            else if (powerup is LevelEndPowerup)
            {
                PowerupManager.IsLevelEndPowerupActivated = true;
            }

            powerup.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Spikes>())
        {
            PowerupManager.IsMagnetPowerupActivated = false;
            this.gameObject.SetActive(false);
        }
    }
}
