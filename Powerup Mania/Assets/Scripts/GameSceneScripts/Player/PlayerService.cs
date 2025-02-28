public class PlayerService
{
    private PlayerController _playerController;

    public PlayerService(ReferenceScriptableObject playerReference)
    {
        _playerController = playerReference.playerController;
    }

    public PlayerLifeStatus GetPlayerLifeStatus() => _playerController.GetPlayerLifeStatus();

    public void SetPlayerLifeStatus(PlayerLifeStatus status) => _playerController.SetPlayerLifeStatus(status);
}
