using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnEnable()
    {
        if (SpikeManager.Instance != null)
        {
            SpikeManager.Instance.RegisterSpike(transform);
        }
        else
        {
            Debug.LogError("SpikeManager instance is null. Ensure that SpikeManager is present in the scene.");
        }
    }

    private void OnDisable()
    {
        SpikeManager.Instance.UnregisterSpike(transform);
    }
}
