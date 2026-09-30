using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int Current { get; private set; }
    public int Max { get; private set; }

    public event Action<int> OnChanged;
    public event Action OnPointsOver;
    public event Action OnRevive;

    public void Heal(int amount)
    {
        if (amount <= 0 || !IsAlive || Current == Max)
            return;

        Current = Math.Min(Current + amount, Max);
        OnChanged?.Invoke(Current);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || !IsAlive)
            return;

        Current = Math.Max(Current - damage, 0);

        OnChanged?.Invoke(Current);
        if (Current == 0)
            OnPointsOver?.Invoke();
    }

    public void SetMax(int newMax, bool fillToMax = false)
    {
        newMax = Math.Max(1, newMax);
        if (newMax == Max)
            return;

        int oldHP = Current;
        Max = newMax;
        if (IsAlive)
        {
            Current = fillToMax ? Max : Math.Min(Current, Max);
            OnChanged?.Invoke(Current);
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
        OnChanged?.Invoke(Current);
        OnRevive?.Invoke();
    }

    public bool IsAlive => Current > 0;
}
