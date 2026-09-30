using UnityEngine;

public abstract class AttackerUpgrader<T> : Upgrader<T>
{
    [SerializeField] private Attacker _attacker;

    protected Attacker Attacker => _attacker;
}