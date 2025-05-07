using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PowerupData
{
    public PowerupType PowerupType;
    public PowerupView PowerupView;
    public int NumbersToSpawn;
    public List<IDAndSpawnPositions> IDAndSpawnPositions;
}

[System.Serializable]
public struct IDAndSpawnPositions
{
    public int PowerupID;
    public Vector3 Position;
}

public enum PowerupType
{
    Time_Rewind,
    Invisibility,
    Abrupt_Level_End,
    Magnet,
    Portal,
    Back_To_Level_1
}
