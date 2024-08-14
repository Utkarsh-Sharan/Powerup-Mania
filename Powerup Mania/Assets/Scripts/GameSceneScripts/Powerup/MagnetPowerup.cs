using UnityEngine;

public class MagnetPowerup : Powerup
{
    protected override void OnEnable()
    {
        PowerupManager.Instance.RegisterPowerup(this);
    }
}
