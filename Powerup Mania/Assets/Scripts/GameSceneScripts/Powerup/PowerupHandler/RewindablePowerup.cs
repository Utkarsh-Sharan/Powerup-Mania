using UnityEngine;

//using abstract class instead of interface as unity won't allow to use built-in methods in interface
public abstract class RewindablePowerup : MonoBehaviour
{
    protected abstract void OnEnable();
}
