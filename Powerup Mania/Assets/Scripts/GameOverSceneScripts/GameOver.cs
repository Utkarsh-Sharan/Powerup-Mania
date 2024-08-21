using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;

    private void Start()
    {
        _restartButton.onClick.AddListener(LoadLevel1);
        _menuButton.onClick.AddListener(GoToMenu);
    }

    private void LoadLevel1()
    {
        SceneManager.LoadScene(2);
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
