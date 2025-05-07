using UnityEngine;

public class PortalPowerup : MonoBehaviour
{
    private static int _powerupID = 2;

    private void Start()
    {
        if (LevelManager.Instance.IsPowerupCollected(_powerupID))
        {
            Destroy(this.gameObject);
        }
    }

    public static int GetPowerupID()
    {
        return _powerupID;
    }
}
