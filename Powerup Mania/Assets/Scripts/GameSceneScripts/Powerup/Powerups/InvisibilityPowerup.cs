using UnityEngine;

public class InvisibilityPowerup : RewindablePowerup
{
    [SerializeField] private int _powerupID;

    protected override void OnEnable()
    {
        PowerupManager.Instance.RegisterRewindablePowerup(this);
        if (LevelManager.Instance.IsPowerupCollected(_powerupID))
        {
            this.gameObject.SetActive(false);
        }
    }

    public int GetPowerupID()
    {
        return _powerupID;
    }
}
