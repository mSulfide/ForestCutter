using System;
using UnityEngine;

[Serializable]
public class Upgradable<T>
{
    [SerializeField] private T _baseValue;

    private UpgradeContainer<T> _upgrades;

    private UpgradeContainer<T> Upgrades => _upgrades ??= new();

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
        Upgrades.Add(upgrade);
    }

    public void Remove(Upgrade<T> upgrade)
    {
        Upgrades.Remove(upgrade);
    }

    public T GetValue()
    {
        T value = _baseValue;
        foreach (Upgrade<T> upgrade in Upgrades)
            value = upgrade.Modificate(value);
        return value;
    }
}