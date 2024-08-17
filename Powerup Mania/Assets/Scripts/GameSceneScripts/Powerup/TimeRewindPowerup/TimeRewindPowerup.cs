using UnityEngine;

public class TimeRewindPowerup : MonoBehaviour
{
    private static int _powerupID = 1;

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
