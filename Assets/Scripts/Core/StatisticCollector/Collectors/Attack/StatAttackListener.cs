using UnityEngine;

[RequireComponent(typeof(Health))]
public class StatAttackListener : MonoBehaviour
{
    private Health _health;

    private void AttackHandler(AttackInfo info)
    {
        StatAttackInfo stat = new()
        {
            Damage = info.Damage,
            Health = info.Target?.Current ?? 0,
            Force = info.Force,
            Armor = info.ArmorIgnore
        };

        Context.Stats.AddRecord(stat);
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.OnAttacked += AttackHandler;
    }

    private void OnDisable()
    {
        _health.OnAttacked -= AttackHandler;
    }
}