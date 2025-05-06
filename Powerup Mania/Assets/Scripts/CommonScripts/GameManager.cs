using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } set { _instance = value; } }

    [Header("Player Properties")]
    [SerializeField] private PlayerScriptableObject _playerSO;
    [SerializeField] private PlayerView _playerView;

    private PlayerService _playerService;

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

    private void Start()
    {
        CreateServices();
    }

    private void CreateServices()
    {
        _playerService = new PlayerService(_playerSO, _playerView);
    }

    public void LoadGameOverScene(GameOverType gameOverType)
    {
        switch(gameOverType)
        {
            case GameOverType.ABRUPT_GAME_OVER:
                LevelManager.Instance.ClearAllHashSets();
                SoundManager.Instance.PlayMusic(Sounds.ABRUPT_GAME_END);
                SceneManager.LoadScene(4);
                break;

            case GameOverType.TIME_REWIND_GAME_OVER:
                LevelManager.Instance.ClearAllHashSets();
                SoundManager.Instance.PlayMusic(Sounds.GAME_OVER);
                SceneManager.LoadScene(5);
                break;

            case GameOverType.GAME_WIN:
                LevelManager.Instance.ClearAllHashSets();
                SoundManager.Instance.PlayMusic(Sounds.GAME_WIN);
                SceneManager.LoadScene(6);
                break;
        }
    }

    public PlayerService GetPlayerService() => _playerService;
}