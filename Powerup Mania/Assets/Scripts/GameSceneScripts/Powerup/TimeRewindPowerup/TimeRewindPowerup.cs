using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindPowerup : MonoBehaviour
{
    private float _rewindDuration = 10f; // Duration in seconds to rewind

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            // Trigger the rewind effect
            //StartCoroutine(RewindCoroutine());
            PowerupManager.IsTimeRewindActivated = true;

            Destroy(this.gameObject);
        }
    }

    //private IEnumerator RewindCoroutine()
    //{
    //    // Wait for a short moment to ensure all actions are processed
    //    yield return new WaitForSeconds(0.1f);
        
    //    // Rewind the game state
    //    float startTime = Time.time;
    //    while (Time.time - startTime < _rewindDuration)
    //    {
    //        TimeManager.Instance.RewindState();
    //        yield return new WaitForSeconds(0.05f); // Adjust the delay as needed
    //    }
    //}
}
