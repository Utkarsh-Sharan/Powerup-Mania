using UnityEngine;

public class LevelEndPowerup : MonoBehaviour
{
    private static int _powerupID = 6;

    private void Start()
    {
        if (LevelManager.Instance.IsPowerupCollected(_powerupID))
        {
            Destroy(gameObject);
        }
    }

    public static int GetPowerupID()
    {
        return _powerupID;
    }
}
