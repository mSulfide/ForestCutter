using System;
using UnityEngine;

public class SpawnedEntity : MonoBehaviour
{
    private Action _onDestroy;
    private bool _released;

    public void Initialize(Action onDestroy)
    {
        _onDestroy = onDestroy;
    }

    public void Release()
    {
        if (_released)
            return;
        _released = true;

        _onDestroy?.Invoke();
    }

    private void OnDestroy()
    {
        Release();
    }
}