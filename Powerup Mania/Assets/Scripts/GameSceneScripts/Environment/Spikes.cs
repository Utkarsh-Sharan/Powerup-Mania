using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public Transform spikeTransform;

    private void Start()
    {
        spikeTransform = this.transform;
    }
}
