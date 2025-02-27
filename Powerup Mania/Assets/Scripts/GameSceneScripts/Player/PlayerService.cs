public class PlayerService
{
    private PlayerController _playerController;

    public PlayerService(ReferenceScriptableObject playerReference)
    {
        _playerController = playerReference.playerController;
    }
}
