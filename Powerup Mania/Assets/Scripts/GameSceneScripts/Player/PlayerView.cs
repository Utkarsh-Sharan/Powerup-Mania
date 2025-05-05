using System.Collections;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;

    private PlayerController _playerController;
    private Coroutine _deathCountdownCoroutine;

    public void Initialize(PlayerController playerController)
    {
        _playerController = playerController;
    }

    private void Start()
    {
        if (LevelManager.Instance.playerCameBackFromPortalLevel)
        {
            LevelManager.Instance.playerCameBackFromPortalLevel = false;
            transform.position = LevelManager.playerLastPosition;
        }
        else
        {
            transform.position = Vector3.zero;
        }
    }

    private void Update()
    {
        _playerController.UpdatePlayer();
    }

    public Coroutine StartDeathCountdownRoutine(float countdownDuration)
    {
        return StartCoroutine(DeathCountdownRoutine(countdownDuration));
    }

    public void StopDeathCountdownRoutine()
    {
        StopCoroutine(_deathCountdownCoroutine);
        _deathCountdownCoroutine = null;
    }

    private IEnumerator DeathCountdownRoutine(float countdownDuration)
    {
        float timeLeft = countdownDuration;

        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;

            // If player comes back to life during countdown, stop the Coroutine
            if (_playerController.PlayerLifeStatus == PlayerLifeStatus.ALIVE)
            {
                _deathCountdownCoroutine = null;
                yield break;
            }
        }

        _playerController.PlayerLifeStatus = PlayerLifeStatus.DEAD;
        GameManager.Instance.LoadGameOverScene(GameOverType.TIME_REWIND_GAME_OVER);
    }

    public void HandlePlayerAlpha(bool isPlayerAlive)
    {
        if (isPlayerAlive)
        {
            Color playerColor = _playerSpriteRenderer.color;
            playerColor.a = 1;
            _playerSpriteRenderer.color = playerColor;
        }
        else
        {
            Color playerColor = _playerSpriteRenderer.color;
            playerColor.a = 0;
            _playerSpriteRenderer.color = playerColor;
        }
    }

    public Transform GetPlayerTransform() => transform;
}
