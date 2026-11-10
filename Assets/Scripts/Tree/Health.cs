using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void PointsChangeAction(int current, int max);

    public int Current { get; private set; }
    public int Max { get; private set; }

    public event Attacker.AttackAction OnAttacked;
    public event PointsChangeAction OnChanged;
    public event Action OnDied;
    public event Action OnRevive;

    public virtual void TakeAttack(AttackInfo info)
    {
        OnAttacked?.Invoke(info);
        TakeDamage(info.Damage);
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || !IsAlive || Current == Max)
            return;

        Current = Math.Min(Current + amount, Max);
        OnChanged?.Invoke(Current, Max);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || !IsAlive)
            return;

        Current = Math.Max(Current - damage, 0);

        OnChanged?.Invoke(Current, Max);
        if (Current == 0)
            OnDied?.Invoke();
    }

    public void SetMax(int newMax, bool fillToMax = false)
    {
        Max = Math.Max(1, newMax);
        if (IsAlive)
        {
            Current = fillToMax ? Max : Math.Min(Current, Max);
            OnChanged?.Invoke(Current, Max);
        }
        else if (fillToMax)
        {
            Revive();
        }
    }

    public void Revive(int healthPoints = int.MaxValue)
    {
        if (IsAlive)
            return;
        Current = Math.Clamp(healthPoints, 1, Max);
        OnChanged?.Invoke(Current, Max);
        OnRevive?.Invoke();
    }

    public bool IsAlive => Current > 0;
}
