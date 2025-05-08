using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericMonoSingleton<GameManager>
{
    [Header("Player Properties")]
    [SerializeField] private PlayerView _playerView;

    [Header("Scriptable Objects")]
    [SerializeField] private PlayerScriptableObject _playerSO;
    [SerializeField] private LevelScriptableObject _levelSO;

    private LevelService _levelService;
    private PlayerService _playerService;

    protected override void Awake()
    {
        base.Awake();

        CreateServices();
    }

    private void CreateServices()
    {
        _levelService = new LevelService(_levelSO);
        _playerService = new PlayerService(_playerSO, _playerView);
    }

    public void LoadGameOverScene(GameOverType gameOverType)
    {
        LevelManager.Instance.ClearAllHashSets();

        switch (gameOverType)
        {
            case GameOverType.ABRUPT_GAME_OVER:
                SoundManager.Instance.PlayMusic(Sounds.ABRUPT_GAME_END);
                SceneManager.LoadScene(4);
                break;

            case GameOverType.TIME_REWIND_GAME_OVER:
                SoundManager.Instance.PlayMusic(Sounds.GAME_OVER);
                SceneManager.LoadScene(5);
                break;

            case GameOverType.GAME_WIN:
                SoundManager.Instance.PlayMusic(Sounds.GAME_WIN);
                SceneManager.LoadScene(6);
                break;
        }
    }

    public PlayerService GetPlayerService() => _playerService;
}