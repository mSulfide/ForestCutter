using UnityEngine;
using UnityEngine.InputSystem;

public class MyDebug : MonoBehaviour
{
    public AutoAttacker AutoAttacker;

    private void Update()
    {
        Debug.Log(AutoAttacker.Overheat);
    }
}
