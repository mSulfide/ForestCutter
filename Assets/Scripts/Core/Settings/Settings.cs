using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    private Dictionary<EAction, KeyCode> _keyBindings = new();

    public KeyCode GetKey(EAction action)
    {
        if (_keyBindings.TryGetValue(action, out KeyCode key))
            return key;
        else
            return action switch
            {
                EAction.Attack => KeyCode.Mouse0,
                _ => throw new System.NotImplementedException()
            };
    }
}