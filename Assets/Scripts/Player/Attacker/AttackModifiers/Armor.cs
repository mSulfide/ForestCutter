using UnityEngine;

public class Armor : MonoBehaviour, IAttackModifire
{
    [SerializeField, Range(0f, 1f)] private float _armor = 0f;
    [SerializeField] private int _damageThreshold = 0;

    public AttackInfo Modificate(AttackInfo info)
    {
        info.Damage = Mathf.RoundToInt((info.Damage - _damageThreshold) * (1 - Mathf.Clamp(_armor - info.ArmorIgnore, 0f, 1f)));

        return info;
    }
}