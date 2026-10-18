using System.Linq;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public delegate void AttackAction(AttackInfo info);

    [SerializeField] private Player _player;
    [SerializeField] private Upgradable<int> _baseDamage = new(1);
    [SerializeField] private Upgradable<float> _cooldown = new(1f);
    [SerializeField, Min(0f)] private float _autoAimRange = 0f;
    [SerializeField] private AnimationCurve _damageByTime = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    private float _lastAttackTimestamp;
    private AttackInfo _lastAttack;
    private readonly Upgradable<AttackInfo> _attack = new(default);

    public Upgradable<int> BaseDamage => _baseDamage;

    public Upgradable<float> Cooldown => _cooldown;

    public Upgradable<AttackInfo> Attack => _attack;

    public event AttackAction OnAttackAction;

    private void AttackByRay(Ray ray)
    {
        RaycastHit? hit = Physics.Raycast(ray, out RaycastHit hitInfo) ? hitInfo : null;
        Health aim = (hit.HasValue && hit.Value.collider.TryGetComponent(out Health health) && health.IsAlive) ? health : null;

        if (aim == null && _autoAimRange >= 0f)
        {
            float min = float.MaxValue;
            foreach (RaycastHit sphereHit in Physics.SphereCastAll(ray, _autoAimRange).Where(hitInfo => hitInfo.collider.TryGetComponent(out Health health) && health.IsAlive))
            {
                float sqrDistance = GetSqrDistance(ray, sphereHit.point);
                if (sqrDistance < min)
                {
                    min = sqrDistance;
                    hit = sphereHit;
                    aim = sphereHit.collider.GetComponent<Health>();
                }
            }
        }

        if (aim != null)
        {
            float force = _lastAttack.Cooldown > 0f ? _damageByTime.Evaluate((Time.time - _lastAttackTimestamp) / _lastAttack.Cooldown) : 1f;
            int damage = Mathf.RoundToInt(_baseDamage.GetValue() * force);

            _attack.BaseValue = new()
            {
                Damage = damage,
                Target = aim,
                Position = hit?.point ?? aim.transform.position,
                Cooldown = _cooldown.GetValue(),
                Force = force
            };

            _lastAttackTimestamp = Time.time;

            AttackInfo info = _attack.GetValue();

            foreach (var modifire in info.Target.GetComponents<IAttackModifire>())
                info = modifire.Modificate(info);

            OnAttackAction?.Invoke(info);

            aim.TakeAttack(info);

            _lastAttack = info;
        }
    }

    private float GetSqrDistance(Ray ray, Vector3 position)
    {
        float volume = ray.direction.z * (ray.origin.x - position.x) - ray.direction.x * (ray.origin.z - position.z);
        float sqrSquare = ray.direction.x * ray.direction.x + ray.direction.z * ray.direction.z;
        return volume * volume / sqrSquare;
    }

    private void OnEnable()
    {
        _player.OnAttackClick += AttackByRay;
    }

    private void OnDisable()
    {
        _player.OnAttackClick -= AttackByRay;
    }
}