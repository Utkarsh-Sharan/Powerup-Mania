using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } set { _instance = value; } }

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

    public void LoadGameOverScene(GameOverType gameOverType)
    {
        switch(gameOverType)
        {
            case GameOverType.ABRUPT_GAME_OVER:
                Debug.Log("Hi from abrupt");
                SceneManager.LoadScene(4);
                break;

            case GameOverType.TIME_REWIND_GAME_OVER:
                SceneManager.LoadScene(5);
                break;

            case GameOverType.GAME_WIN:
                SceneManager.LoadScene(6);
                break;
        }
    }
}

public enum GameOverType
{
    ABRUPT_GAME_OVER,
    TIME_REWIND_GAME_OVER,
    GAME_WIN
}