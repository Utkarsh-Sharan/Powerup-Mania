using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;
    public static TimeManager Instance { get { return _instance; } set { _instance = value; } }

    [SerializeField] private Transform _playerTransform; // Reference to the player's Transform
    [SerializeField] private PowerupManager _powerupManager; //Reference to powerup manager

    private List<GameState> stateList = new List<GameState>(); // Use a List for flexibility
    private float _saveInterval = 1f; // Time in seconds between saves
    private float _nextSaveTime;

    private int _maxStates = 10; // Maximum number of states to keep in the list

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (_playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned!");
        }
    }

    private void Update()
    {
        if (Time.time >= _nextSaveTime)
        {
            SaveState();
            _nextSaveTime = Time.time + _saveInterval;
        }
    }

    private void SaveState()
    {
        GameState state = new GameState();
        state.Save(_playerTransform, _powerupManager);

        // Ensure the list does not exceed the maximum size
        if (stateList.Count >= _maxStates)
        {
            // Remove the oldest state (first element)
            stateList.RemoveAt(0);
        }

        // Add the new state
        stateList.Add(state); 
    }

    public float StateListCount()
    {
        return stateList.Count;
    }

    public void RewindState()
    {
        if (stateList.Count > 0)
        {
            GameState state = stateList[stateList.Count - 1];     // Get the most recent state (last element)
            stateList.RemoveAt(stateList.Count - 1);              // Remove the most recent state
            state.Restore(_playerTransform, _powerupManager);
        }
    }

    public void ClearStateList()
    {
        stateList.Clear();
    }
}

[System.Serializable]
public class GameState
{
    private Vector2 playerPosition;
    private Dictionary<string, bool> _powerups;
    private Dictionary<string, Vector2> _powerupPositions;

    public GameState()
    {
        _powerups = new Dictionary<string, bool>();
        _powerupPositions = new Dictionary<string, Vector2>();
    }

    public void Save(Transform playerTransform, PowerupManager powerupManager)
    {
        //saving player positions
        playerPosition = playerTransform.position;

        //saving powerup states
        _powerups["Magnet"] = PowerupManager.IsMagnetPowerupActivated;
        _powerups["Invisibility"] = PowerupManager.IsInvisibilityPowerupActivated;

        //saving powerup positions
        foreach(var powerup in powerupManager.GetAllPowerups())
        {
            _powerupPositions[powerup.name] = powerup.transform.position;
        }
    }

    public void Restore(Transform playerTransform, PowerupManager powerupManager)
    {
        //restoring player positions
        playerTransform.position = playerPosition;

        //restoring powerup states
        PowerupManager.IsMagnetPowerupActivated = _powerups.GetValueOrDefault("Magnet", false);
        PowerupManager.IsInvisibilityPowerupActivated = _powerups.GetValueOrDefault("Invisibility", false);
        
        //restoring powerup positions
        foreach(var powerup in powerupManager.GetAllPowerups())
        {
            if(_powerupPositions.TryGetValue(powerup.name, out Vector2 position))
            {
                powerup.transform.position = position;
                powerup.gameObject.SetActive(true);
            }
        }
    }
}