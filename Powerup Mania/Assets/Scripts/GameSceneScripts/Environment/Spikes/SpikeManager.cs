using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeManager : MonoBehaviour
{
    public static SpikeManager Instance { get; private set; }

    private List<Transform> _spikes = new List<Transform>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  //will look at this if it will be necessary
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterSpike(Transform spikeTransform)
    {
        if (!_spikes.Contains(spikeTransform))
        {
            _spikes.Add(spikeTransform);
        }
    }

    public void UnregisterSpike(Transform spikeTransform)
    {
        if (_spikes.Contains(spikeTransform))
        {
            _spikes.Remove(spikeTransform);
        }
    }

    public List<Transform> GetSpikes()
    {
        return _spikes;
    }
}
