using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } set { _instance = value; } }

    [SerializeField] private Transform _playerTransform;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (PowerupManager.IsPortalPowerupActivated)
        {
            PowerupManager.IsPortalPowerupActivated = false;

            TimeManager.Instance.ClearStateList();  //clearing old states for new level

            SceneManager.LoadScene(1);
        }

        if (PowerupManager.IsBackToLevel1PowerupActivated)
        {
            PowerupManager.IsBackToLevel1PowerupActivated = false;

            TimeManager.Instance.ClearStateList();  //clearing old states for new level

            SceneManager.LoadScene(0);
        }
    }
}