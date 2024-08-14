using UnityEngine;

public class MagnetPowerup : RewindablePowerup
{
    protected override void OnEnable()
    {
        PowerupManager.Instance.RegisterRewindablePowerup(this);
    }
}
