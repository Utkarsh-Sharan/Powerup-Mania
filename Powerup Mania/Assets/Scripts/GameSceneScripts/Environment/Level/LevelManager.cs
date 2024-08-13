using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _environmentObjects = new List<GameObject>();
    [SerializeField] private Image _fadeInAndOutImage;

    private bool _isTransitioning;

    private void Update()
    {
        if (PowerupManager.IsPortalPowerupActivated && !_isTransitioning)
        {
            StartCoroutine(HandleLevelTransition());
            PowerupManager.IsPortalPowerupActivated = false;
        }
    }

    private IEnumerator HandleLevelTransition()
    {
        _isTransitioning = true;

        // Fade in
        yield return StartCoroutine(Fade(_fadeInAndOutImage, 0f, 1f, 1f));

        // Swap environment objects
        _environmentObjects[0].SetActive(!_environmentObjects[0].activeSelf);
        _environmentObjects[1].SetActive(!_environmentObjects[1].activeSelf);

        // Fade out
        yield return StartCoroutine(Fade(_fadeInAndOutImage, 1f, 0f, 1f));

        _isTransitioning = false;
    }

    private IEnumerator Fade(Image image, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = image.color;
        color.a = startAlpha;
        image.color = color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            image.color = color;
            yield return null;
        }

        color.a = endAlpha;
        image.color = color;
    }

}
