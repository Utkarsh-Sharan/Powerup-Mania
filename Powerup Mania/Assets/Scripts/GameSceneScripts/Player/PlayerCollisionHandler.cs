using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var rewindablePowerup = other.GetComponent<RewindablePowerup>();

        if (rewindablePowerup != null)
        {
            if (rewindablePowerup is MagnetPowerup)
            {
                PowerupManager.IsMagnetPowerupActivated = true;
            }
            else if (rewindablePowerup is InvisibilityPowerup)
            {
                PowerupManager.IsInvisibilityPowerupActivated = true;
            }

            rewindablePowerup.gameObject.SetActive(false);
        }

        if (other.GetComponent<TimeRewindPowerup>())
        {
            PowerupManager.IsTimeRewindActivated = true;

            LevelManager.Instance.CollectPowerup(TimeRewindPowerup.GetPowerupID());

            Destroy(other.gameObject);
        }
        else if (other.GetComponent<PortalPowerup>())
        {
            PowerupManager.IsPortalPowerupActivated = true;

            LevelManager.Instance.CollectPowerup(PortalPowerup.GetPowerupID());
            LevelManager.playerLastPosition = this.transform.position;

            Destroy(other.gameObject);
        }
        else if (other.GetComponent<BackToLevel1Powerup>())
        {
            PowerupManager.IsBackToLevel1PowerupActivated = true;

            Destroy(other.gameObject);
        }
        else if (other.GetComponent<LevelEndPowerup>())
        {
            PowerupManager.IsLevelEndPowerupActivated = true;

            Destroy(other.gameObject);
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
