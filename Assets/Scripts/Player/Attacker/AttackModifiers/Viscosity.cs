using UnityEngine;

public class Viscosity : MonoBehaviour, IAttackModifire
{
    [SerializeField, Min(0f)] private float _cooldownModifire = 0f;

    public float WaitCooldown { get; set; }

    public AttackInfo Modificate(AttackInfo info)
    {
        info.Cooldown += WaitCooldown;
        WaitCooldown = _cooldownModifire;

        return info;
    }

    private void Start()
    {
        WaitCooldown = _cooldownModifire;
    }
}