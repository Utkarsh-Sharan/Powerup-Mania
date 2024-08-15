using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private void Update()
    {
        if (PowerupManager.IsPortalPowerupActivated)
        {
            //StartCoroutine(HandleLevelTransition());
            PowerupManager.IsPortalPowerupActivated = false;
            TimeManager.Instance.ClearStateList();

            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator HandleLevelTransition()
    {
        //_isTransitioning = true;
        //TimeManager.Instance.ClearStateList();  //will clear state list from previous level and start saving current level state list

        //// Fade in
        //yield return StartCoroutine(Fade(_fadeInAndOutImage, 0f, 1f, 1f));

        //// Swapping environment objects
        //_environmentObjects[0].SetActive(!_environmentObjects[0].activeSelf);
        //_environmentObjects[1].SetActive(!_environmentObjects[1].activeSelf);

        //// Fade out
        yield return new WaitForSeconds(1);

        //_isTransitioning = false;
    }
}
