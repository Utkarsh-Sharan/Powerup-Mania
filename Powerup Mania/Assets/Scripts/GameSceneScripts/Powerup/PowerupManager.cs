using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private static bool _isTimeRewindActivated;
    public static bool IsTimeRewindActivated { get { return _isTimeRewindActivated; } set { _isTimeRewindActivated = value; } }

    private static bool _isMagnetPowerupActivated;
    public static bool IsMagnetPowerupActivated { get { return _isMagnetPowerupActivated; } set { _isMagnetPowerupActivated = value; } }
}
