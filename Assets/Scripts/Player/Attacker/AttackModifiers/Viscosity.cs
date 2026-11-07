using UnityEngine;

public class Viscosity : MonoBehaviour, IAttackModifire
{
    [SerializeField, Min(0f)] private float _cooldownModifire = 0f;

    public AttackInfo Modificate(AttackInfo info)
    {
        info.Cooldown += _cooldownModifire * info.Cooldown;

        return info;
    }
}