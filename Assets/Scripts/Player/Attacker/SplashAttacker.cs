using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class SplashAttacker : MonoBehaviour
{
    [SerializeField] private Upgradable<float> _range = new(1f);
    [SerializeField] private Upgradable<float> _damageMultiplayer = new(0.5f);

    private Attacker _attacker;

    private void SplashAttack(Vector3 position, Health exception, int damage)
    {
        foreach (Collider collider in Physics.OverlapSphere(position, _range.GetValue()))
            if (collider.TryGetComponent(out Health aim) && aim != exception)
                aim.TakeDamage(Mathf.CeilToInt(damage * _damageMultiplayer.GetValue()));
    }

    private void AttackHandler(AttackInfo info)
    {
        SplashAttack(info.Position, info.Target, info.Damage);
    }

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        _attacker.OnAttack += AttackHandler;
    }

    private void OnDisable()
    {
        _attacker.OnAttack -= AttackHandler;
    }
}