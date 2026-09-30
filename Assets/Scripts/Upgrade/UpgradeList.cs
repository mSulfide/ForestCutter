using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeList : ScriptableObject
{
    public abstract int Count { get; }
}

public abstract class UpgradeList<T> : UpgradeList
{
    [SerializeField] private List<T> _modifiers = new();

    public override int Count => _modifiers.Count;

    public T this[int index]
    {
        get => _modifiers[Mathf.Clamp(index, 0, _modifiers.Count - 1)];
    }
}