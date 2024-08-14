using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPowerupEffectHandler : MonoBehaviour
{
    //coroutine variable as i am calling coroutine in update
    private bool _isCoroutineActive;

    //time rewind powerup
    private float _rewindDuration = 10f; // Duration in seconds to rewind

    //magnet powerup
    private float _magnetRange = 5f;
    private float _magnetSpeed = 0.1f;

    //invisibility powerup
    private SpriteRenderer _playerSpriteRenderer;
    private float _invisibilityDuration = 5f;

    private void Start()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_isCoroutineActive && Input.GetKeyDown(KeyCode.T) && PowerupManager.IsTimeRewindActivated)
        {
            _isCoroutineActive = true;
            StartCoroutine(RewindRoutine());
        }

        if (PowerupManager.IsMagnetPowerupActivated)
        {
            MoveTowardsSpikes();
        }

        if (!_isCoroutineActive && PowerupManager.IsInvisibilityPowerupActivated)
        {
            _isCoroutineActive = true;
            StartCoroutine(InvisibilityOverRoutine());
        }

        if(!_isCoroutineActive && PowerupManager.IsLevelEndPowerupActivated)
        {
            _isCoroutineActive = true;
            SceneManager.LoadScene(1);
            PowerupManager.IsLevelEndPowerupActivated = false;
        }
    }


    private IEnumerator RewindRoutine()
    {
        // Waiting for a short moment to ensure all actions are processed
        yield return new WaitForSeconds(0.1f);

        // Rewinding the game state
        float startTime = Time.time;
        while (Time.time - startTime < _rewindDuration)
        {
            TimeManager.Instance.RewindState();
            yield return new WaitForSeconds(0.1f);
        }

        _isCoroutineActive = false;
    }

    private void MoveTowardsSpikes()
    {
        List<Transform> spikes = SpikeManager.Instance.GetSpikes();

        foreach (Transform spikeTransform in spikes)
        {
            float distance = Vector2.Distance(spikeTransform.position, transform.position);

            if (distance < _magnetRange)
            {
                transform.position = Vector2.MoveTowards(spikeTransform.position, transform.position, _magnetSpeed * Time.deltaTime);
                break;
            }
        }
    }

    private IEnumerator InvisibilityOverRoutine()
    {
        Color playerColor = _playerSpriteRenderer.color;
        playerColor.a = 0.3f;
        _playerSpriteRenderer.color = playerColor;

        yield return new WaitForSeconds(_invisibilityDuration);

        playerColor.a = 1f;
        _playerSpriteRenderer.color = playerColor;

        _isCoroutineActive = false;
        PowerupManager.IsInvisibilityPowerupActivated = false;
    }
}
