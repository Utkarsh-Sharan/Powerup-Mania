using UnityEngine;

public class InvisibilityPowerup : RewindablePowerup
{
    protected override void OnEnable()
    {
        PowerupManager.Instance.RegisterRewindablePowerup(this);
    }
}
