using UnityEngine;

public class LevelEndPowerup : Powerup
{
    protected override void OnEnable()
    {
        PowerupManager.Instance.RegisterPowerup(this);
    }

    protected override void OnDisable()
    {
        PowerupManager.Instance.UnregisterPowerup(this);
    }
}
