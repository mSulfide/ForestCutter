using UnityEngine;

public abstract class AutoAttackerUpgrader<T, TUpgradeList> : Upgrader<T, TUpgradeList> where TUpgradeList : UpgradeList<T>
{
    [SerializeField] private AutoAttacker _autoAttacker;

    protected AutoAttacker AutoAttacker => _autoAttacker;
}

public abstract class AutoAttackerUpgrader<T> : AutoAttackerUpgrader<T, UpgradeList<T>> { }