using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attacker : MonoBehaviour
{
    public delegate void AttackAction(AttackInfo info);

    [SerializeField] private Upgradable<int> _baseDamage = new(1);
    [SerializeField] private Upgradable<float> _cooldown = new(1f);
    [SerializeField, Min(0f)] private float _autoAimRange = 0f;
    [SerializeField] private AnimationCurve _damageByTime = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    private Camera _mainCamera;
    private float _lastAttackTimestamp;
    private readonly Upgradable<AttackInfo> _attack = new(default);

    public Upgradable<int> BaseDamage => _baseDamage;

    public Upgradable<float> Cooldown => _cooldown;

    public Upgradable<AttackInfo> Attack => _attack;

    public event AttackAction OnAttackAction;

    public void OnAttack()
    {
        AttackByRay(_mainCamera.ScreenPointToRay(Input.mousePosition));
    }

    private float GetSqrDistance(Ray ray, Vector3 position)
    {
        float volume = ray.direction.z * (ray.origin.x - position.x) - ray.direction.x * (ray.origin.z - position.z);
        float sqrSquare = ray.direction.x * ray.direction.x + ray.direction.z * ray.direction.z;
        return volume * volume / sqrSquare;
    }

    private void AttackByRay(Ray ray)
    {
        RaycastHit? hit = Physics.Raycast(ray, out RaycastHit hitInfo) ? hitInfo : null;
        Health aim = (hit.HasValue && hit.Value.collider.TryGetComponent(out Health health)) ? health : null;

        if (aim == null && _autoAimRange >= 0f)
        {
            float min = float.MaxValue;
            foreach (RaycastHit sphereHit in Physics.SphereCastAll(ray, _autoAimRange).Where(hitInfo => hitInfo.collider.TryGetComponent(out Health health)))
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
            float cooldown = _cooldown.GetValue();
            float force = _damageByTime.Evaluate((Time.time - _lastAttackTimestamp) / cooldown);
            int damage = Mathf.RoundToInt(_baseDamage.GetValue() * force);

            _attack.BaseValue = new()
            {
                Damage = damage,
                Target = aim,
                Position = hit?.point ?? aim.transform.position,
                Cooldown = cooldown,
                Force = force
            };

            _lastAttackTimestamp = Time.time;

            AttackInfo info = _attack.GetValue();

            foreach (var modifire in info.Target.GetComponents<IAttackModifire>())
                info = modifire.Modificate(info);

            OnAttackAction?.Invoke(info);

            aim.TakeAttack(info);
        }
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }
}