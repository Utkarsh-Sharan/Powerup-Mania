using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance { get; private set; }

    private List<RewindablePowerup> _powerups = new List<RewindablePowerup>();

    private static bool _isTimeRewindActivated;
    public static bool IsTimeRewindActivated { get { return _isTimeRewindActivated; } set { _isTimeRewindActivated = value; } }

    private static bool _isMagnetPowerupActivated;
    public static bool IsMagnetPowerupActivated { get { return _isMagnetPowerupActivated; } set { _isMagnetPowerupActivated = value; } }

    private static bool _isPortalPowerupActivated;
    public static bool IsPortalPowerupActivated { get { return _isPortalPowerupActivated; } set { _isPortalPowerupActivated = value; } }

    private static bool _isInvisibilityPowerupActivated;
    public static bool IsInvisibilityPowerupActivated { get { return _isInvisibilityPowerupActivated; } set { _isInvisibilityPowerupActivated = value; } }

    private static bool _isLevelEndPowerupActivated;
    public static bool IsLevelEndPowerupActivated { get { return _isLevelEndPowerupActivated; } set { _isLevelEndPowerupActivated = value; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterRewindablePowerup(RewindablePowerup powerup)
    {
        if (!_powerups.Contains(powerup))
        {
            _powerups.Add(powerup);
        }
    }

    public IEnumerable<RewindablePowerup> GetAllPowerups()
    {
        return _powerups;
    }
}
