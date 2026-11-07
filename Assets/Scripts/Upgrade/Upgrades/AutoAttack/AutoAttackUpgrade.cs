using UnityEngine;

public class AutoAttackUpgrade : Upgrade<AttackInfo>
{
    private float _damageMultiplier = 1f;
    private float _cooldownMultiplier = 1f;

    public AutoAttackUpgrade(float damage, float cooldown) : base(EUpgradePriority.Multiplier)
    {
        _damageMultiplier = damage;
        _cooldownMultiplier = cooldown;
    }

    public void SetDamage(float damage)
    {
        _damageMultiplier = damage;
    }

    public void SetCooldown(float cooldown)
    {
        _cooldownMultiplier = cooldown;
    }

    public override AttackInfo Modificate(AttackInfo value)
    {
        AttackInfo info = value;

        info.Damage = Mathf.FloorToInt(_damageMultiplier * info.Damage);
        info.Cooldown *= _cooldownMultiplier;

        return info;
    }
}