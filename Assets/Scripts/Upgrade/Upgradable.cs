using System;
using UnityEngine;

[Serializable]
public class Upgradable<T>
{
    [SerializeField] private T _baseValue;

    private readonly UpgradeContainer<T> _upgrades = new();

    public T BaseValue
    {
        get => _baseValue;
        set => _baseValue = value;
    }

    public Upgradable(T baseValue)
    {
        _baseValue = baseValue;
    }

    public void Add(Upgrade<T> upgrade)
    {
        _upgrades.Add(upgrade);
    }

    public void Clear() => Clear(upgrade => true);

    public void Clear(Predicate<Upgrade<T>> isRemove)
    {
        foreach (var upgrade in _upgrades)
            if (isRemove.Invoke(upgrade))
                _upgrades.Remove(upgrade);
    }

    public void Remove(Upgrade<T> upgrade)
    {
        _upgrades.Remove(upgrade);
    }

    public T GetValue()
    {
        T value = _baseValue;
        foreach (Upgrade<T> upgrade in _upgrades)
            value = upgrade.Modificate(value);
        return value;
    }
}