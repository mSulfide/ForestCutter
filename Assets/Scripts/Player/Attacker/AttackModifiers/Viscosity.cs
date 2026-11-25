using UnityEngine;

public class Viscosity : MonoBehaviour, IAttackModifire
{
    private float _cooldownModifire = 0f;

    public void SetCooldownModifire(float modifire)
    {
        _cooldownModifire = Mathf.Max(0f, modifire);
    }

    public AttackInfo Modificate(AttackInfo info)
    {
        info.Cooldown += _cooldownModifire * info.Cooldown;

        return info;
    }
}