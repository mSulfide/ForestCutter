using System;
using UnityEngine;

public abstract class Upgrader : MonoBehaviour
{
    private int _level = -1;

    public int Level => _level;

    public abstract UpgradeList UpgradeList { get; }

    public abstract int MaxLevel { get; }

    public event Action OnLevelUp;

    public abstract void InvokeUpgrade(int level);

    public void LevelUp() => SetLevel(_level + 1);

    public void SetLevel(int level)
    {
        if (_level != level)
        {
            InvokeUpgrade(level);
            _level = level;
            OnLevelUp?.Invoke();
        }
    }
}

public abstract class Upgrader<T, TUpgradeList> : Upgrader where TUpgradeList : UpgradeList<T>
{
    private UpgradeList<T> _upgrades;

    private UpgradeList<T> Upgrades => _upgrades ??= Context.Game.Storage.GetUpgradeList<TUpgradeList>() ?? throw new InvalidOperationException($"{nameof(_upgrades)} can't be null");

    public override UpgradeList UpgradeList => Upgrades;
    
    public override int MaxLevel => Upgrades.Count - 1;

    public sealed override void InvokeUpgrade(int level)
    {
        Invoke(Upgrades[Mathf.Clamp(level, 0, Upgrades.Count - 1)]);
    }

    public abstract void Invoke(T upgrade);
}

public abstract class Upgrader<T> : Upgrader<T, UpgradeList<T>> { }