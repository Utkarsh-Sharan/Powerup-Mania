using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } set { _instance = value; } }

    private HashSet<int> _collectedPowerups = new HashSet<int>();

    [HideInInspector] public static Vector2 playerLastPosition;
    [HideInInspector] public bool playerCameBackFromPortalLevel;

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
            playerCameBackFromPortalLevel = true;
            PowerupManager.IsBackToLevel1PowerupActivated = false;

            TimeManager.Instance.ClearStateList();  //clearing old states for new level

            StartCoroutine(WaitForSomeTimeThenLoadScene());
        }
    }

    private IEnumerator WaitForSomeTimeThenLoadScene()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(0);
    }

    public void CollectPowerup(int powerupID)
    {
        _collectedPowerups.Add(powerupID);
    }

    public bool IsPowerupCollected(int powerupID)
    {
        return _collectedPowerups.Contains(powerupID);  //very efficient as this takes O(1) time, that's why used hash set
    }
}