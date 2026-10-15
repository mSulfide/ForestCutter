using UnityEngine;

public class Armor : MonoBehaviour, IAttackModifire
{
    [SerializeField, Min(0f)] private float _armor = 0f;
    [SerializeField] private int _damageThreshold = 0;

    public AttackInfo Modificate(AttackInfo info)
    {
        info.Damage = _damageThreshold < info.Damage ? Mathf.RoundToInt(info.Damage * (1 - Mathf.Clamp(_armor - info.ArmorIgnore, 0f, 1f))) : 0;

        return info;
    }
}