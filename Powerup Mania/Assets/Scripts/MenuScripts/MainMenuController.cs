using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _instructionsButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _playButton.onClick.AddListener(LoadGameScene);
        _instructionsButton.onClick.AddListener(LoadInstructionsScene);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void LoadGameScene()
    {
        SoundManager.Instance.Play(Sounds.BUTTON_CLICK);
        SceneManager.LoadScene(2);
    }

    private void LoadInstructionsScene()
    {
        SoundManager.Instance.Play(Sounds.BUTTON_CLICK);
        SceneManager.LoadScene(1);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
