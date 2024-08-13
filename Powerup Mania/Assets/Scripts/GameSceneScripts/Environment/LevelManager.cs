using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _environmentObjects = new List<GameObject>();

    private void Update()
    {
        if (PowerupManager.IsPortalPowerupActivated)
        {
            _environmentObjects[0].SetActive(false);
            _environmentObjects[1].SetActive(true);
        }
        else
        {
            _environmentObjects[0].SetActive(true);
            _environmentObjects[1].SetActive(false);
        }
    }
}
