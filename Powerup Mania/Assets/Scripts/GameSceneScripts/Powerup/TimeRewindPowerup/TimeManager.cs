using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;
    public static TimeManager Instance { get { return _instance; } set { _instance = value; } }

    [SerializeField] private Transform _playerTransform; // Reference to the player's Transform

    private List<GameState> _stateList = new List<GameState>(); // Use a List for flexibility
    private float _saveInterval = 1f; // Time in seconds between saves
    private float _nextSaveTime;

    private int _maxStates = 20; // Maximum number of states to keep in the list

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        state.Save(_playerTransform);

        // Ensure the list does not exceed the maximum size
        if (_stateList.Count >= _maxStates)
        {
            // Remove the oldest state (first element)
            _stateList.RemoveAt(0);
        }

        _stateList.Add(state); // Add the new state
    }

    public void RewindState()
    {
        if (_stateList.Count > 0)
        {
            // Get the most recent state (last element)
            GameState state = _stateList[_stateList.Count - 1];
            _stateList.RemoveAt(_stateList.Count - 1); // Remove the most recent state
            state.Restore(_playerTransform);
        }
    }
}

[System.Serializable]
public class GameState
{
    private Vector2 playerPosition;

    // Save the player's current position
    public void Save(Transform playerTransform)
    {
        playerPosition = playerTransform.position;
    }

    // Restore the player's position to the saved state
    public void Restore(Transform playerTransform)
    {
        playerTransform.position = playerPosition;
    }
}