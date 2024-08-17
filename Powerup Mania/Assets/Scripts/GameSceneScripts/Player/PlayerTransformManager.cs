using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformManager : MonoBehaviour
{
    private static PlayerTransformManager _instance;
    public static PlayerTransformManager Instance { get { return _instance; } set { _instance = value; } }

    public Transform playerTransform;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
