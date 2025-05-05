using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObject/PlayerScriptableObject")]
public class PlayerScriptableObject : ScriptableObject
{
    public float CountdownDuration;
    public float FireRate;
    public float PlayerSpeed;
    public float PlayerRotationSpeed;
}
