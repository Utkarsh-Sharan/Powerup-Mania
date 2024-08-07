using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private static bool _isTimeRewindActivated;
    public static bool IsTimeRewindActivated { get { return _isTimeRewindActivated; } set { _isTimeRewindActivated = value; } }
}
