using UnityEngine;

public abstract class AttackerUpgrader<T, TUpgradeList> : Upgrader<T, TUpgradeList> where TUpgradeList : UpgradeList<T>
{
    [SerializeField] private Attacker _attacker;

    protected Attacker Attacker => _attacker;
}

public abstract class AttackerUpgrader<T> : AttackerUpgrader<T, UpgradeList<T>> { }