using UnityEngine;

public class Armor : MonoBehaviour, IAttackModifire
{
    private float _armor = 0f;
    private int _damageThreshold = 0;

    public void SetArmor(float armor)
    {
        _armor = Mathf.Max(0f, armor);
    }

    public void SetDamageThreshold(int damageThreshold)
    {
        _damageThreshold = damageThreshold;
    }

    public AttackInfo Modificate(AttackInfo info)
    {
        info.Damage = _damageThreshold * (1f - info.ArmorIgnore) < info.Damage ? Mathf.RoundToInt(info.Damage * (1 - Mathf.Clamp(_armor - info.ArmorIgnore, 0f, 1f))) : 0;

        return info;
    }
}