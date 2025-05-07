using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "ScriptableObject/LevelScriptableObject")]
public class LevelScriptableObject : ScriptableObject
{
    public List<LevelData> LevelData;
}

[System.Serializable]
public struct LevelData
{
    public LevelName LevelName;
    public List<PowerupData> Powerups;
}