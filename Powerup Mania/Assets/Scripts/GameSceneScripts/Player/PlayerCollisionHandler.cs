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
            SoundManager.Instance.Play(Sounds.PLAYER_COLLECTED_POWERUP);
        }

        if (other.GetComponent<TimeRewindPowerup>())
        {
            PowerupManager.IsTimeRewindActivated = true;

            LevelManager.Instance.CollectPowerup(TimeRewindPowerup.GetPowerupID());

            SoundManager.Instance.Play(Sounds.PLAYER_COLLECTED_POWERUP);
            Destroy(other.gameObject);
        }
        else if (other.GetComponent<PortalPowerup>())
        {
            PowerupManager.IsPortalPowerupActivated = true;

            LevelManager.Instance.CollectPowerup(PortalPowerup.GetPowerupID());
            LevelManager.playerLastPosition = this.transform.position;

            SoundManager.Instance.Play(Sounds.PLAYER_COLLECTED_POWERUP);
            Destroy(other.gameObject);
        }
        else if (other.GetComponent<BackToLevel1Powerup>())
        {
            PowerupManager.IsBackToLevel1PowerupActivated = true;

            SoundManager.Instance.Play(Sounds.PLAYER_COLLECTED_POWERUP);
            Destroy(other.gameObject);
        }
        else if (other.GetComponent<LevelEndPowerup>())
        {
            PowerupManager.IsLevelEndPowerupActivated = true;

            SoundManager.Instance.Play(Sounds.PLAYER_COLLECTED_POWERUP);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Spikes>())
        {
            PowerupManager.IsMagnetPowerupActivated = false;

            PlayerController.playerLifeStatus = PlayerLifeStatus.DEAD;
            SoundManager.Instance.Play(Sounds.EXPLOSION_SFX);
        }
        else if (other.gameObject.GetComponent<Tr01Enemy>())
        {
            PlayerController.playerLifeStatus = PlayerLifeStatus.DEAD;
            SoundManager.Instance.Play(Sounds.EXPLOSION_SFX);
        }
        else if (other.gameObject.GetComponent<BlueTr01Enemy>())
        {
            PlayerController.playerLifeStatus = PlayerLifeStatus.DEAD;
            SoundManager.Instance.Play(Sounds.EXPLOSION_SFX);

            if (PowerupManager.IsInvisibilityPowerupActivated)
            {
                PowerupManager.IsInvisibilityPowerupActivated = false;
            }
        }
    }
}
