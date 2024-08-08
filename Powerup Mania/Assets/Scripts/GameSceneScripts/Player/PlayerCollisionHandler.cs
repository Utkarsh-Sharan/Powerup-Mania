using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<TimeRewindPowerup>())
        {
            PowerupManager.IsTimeRewindActivated = true;

            Destroy(other.gameObject);
        }
        else if (other.GetComponent<MagnetPowerup>())
        {
            PowerupManager.IsMagnetPowerupActivated = true;

            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Spikes>())
        {
            this.gameObject.SetActive(false);
        }
    }
}
