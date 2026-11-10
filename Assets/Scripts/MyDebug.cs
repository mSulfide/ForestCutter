using UnityEngine;
using UnityEngine.InputSystem;

public class MyDebug : MonoBehaviour
{
    [SerializeField] private TreeSettings _boss;
    [SerializeField] private TreeSpawner _spawner;

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _spawner.Spawn(_boss);
        }
    }
}
