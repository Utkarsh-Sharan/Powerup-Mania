public class PlayerService
{
    private PlayerController _playerController;

    public PlayerService(PlayerScriptableObject playerSO, PlayerView playerView)
    {
        _playerController = new PlayerController(playerSO, playerView);
    }
}
