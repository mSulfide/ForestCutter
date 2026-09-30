using UnityEngine;

public class CritAttackUpgrade : Upgrade<AttackInfo>
{
    private float _multiplier;
    private float _chance;

    public CritAttackUpgrade(float multiplier, float chance) : base(EUpgradePriority.Multiplier)
    {
        _multiplier = multiplier;
        _chance = chance;
    }

    public void SetMultiplier(float multiplier)
    {
        _multiplier = Mathf.Max(1f, multiplier);
    }

    public void SetChance(float chance)
    {
        _chance = Mathf.Clamp01(chance);
    }

    public override AttackInfo Modificate(AttackInfo value)
    {
        if (_chance > Random.Range(0f, 1f))
        {
            value.IsCrit = true;
            value.Damage = Mathf.RoundToInt(value.Damage * _multiplier);
        }
        return value;
    }
}