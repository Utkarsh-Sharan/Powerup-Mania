using UnityEngine;

public class PortalPowerup : Powerup
{
    protected override void OnEnable()
    {
        PowerupManager.Instance.RegisterPowerup(this);
    }
}
