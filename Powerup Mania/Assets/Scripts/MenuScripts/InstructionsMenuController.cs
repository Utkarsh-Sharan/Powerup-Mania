using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionsMenuController : MonoBehaviour
{
    [SerializeField] private GameObject[] _instructionSceneObjects; 

    [SerializeField] private Button _controlSectionNextButton;
    [SerializeField] private Button _controlSectionBackButton;

    [SerializeField] private Button _powerupSectionNextButton;
    [SerializeField] private Button _powerupSectionBackButton;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _enemiesSectionBackButton;

    private void Start()
    {
        _controlSectionNextButton.onClick.AddListener(GoToPowerupsSection);
        _controlSectionBackButton.onClick.AddListener(GoToMainMenu);

        _powerupSectionNextButton.onClick.AddListener(GoToEnemiesSection);
        _powerupSectionBackButton.onClick.AddListener(GoToControlsSection);

        _playButton.onClick.AddListener(LoadGameScene);
        _enemiesSectionBackButton.onClick.AddListener(GoToPowerupsSection);
    }

    private void GoToPowerupsSection()
    {
        SoundManager.Instance.Play(Sounds.BUTTON_CLICK);

        _instructionSceneObjects[0].SetActive(false);
        _instructionSceneObjects[1].SetActive(true);
        _instructionSceneObjects[2].SetActive(false);
    }

    private void GoToMainMenu()
    {
        SoundManager.Instance.Play(Sounds.BUTTON_CLICK);

        SceneManager.LoadScene(0);
    }

    private void GoToEnemiesSection()
    {
        SoundManager.Instance.Play(Sounds.BUTTON_CLICK);

        _instructionSceneObjects[0].SetActive(false);
        _instructionSceneObjects[1].SetActive(false);
        _instructionSceneObjects[2].SetActive(true);
    }

    private void GoToControlsSection()
    {
        SoundManager.Instance.Play(Sounds.BUTTON_CLICK);

        _instructionSceneObjects[0].SetActive(true);
        _instructionSceneObjects[1].SetActive(false);
        _instructionSceneObjects[2].SetActive(false);
    }

    private void LoadGameScene()
    {
        SoundManager.Instance.Play(Sounds.BUTTON_CLICK);

        SceneManager.LoadScene(2);
    }
}
