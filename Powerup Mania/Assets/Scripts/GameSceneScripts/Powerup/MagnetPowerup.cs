using UnityEngine;

public class MagnetPowerup : RewindablePowerup
{
    private static int _powerupID = 3;

    protected override void OnEnable()
    {
        PowerupManager.Instance.RegisterRewindablePowerup(this);
        if (LevelManager.Instance.IsPowerupCollected(_powerupID))
        {
            this.gameObject.SetActive(false);
        }
    }

    public static int GetPowerupID()
    {
        return _powerupID;
    }
}
