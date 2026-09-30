using System.Collections;
using System.Linq;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public delegate void AttackAction(AttackInfo info);

    [SerializeField] private Upgradable<int> _baseDamage = new(1);
    [SerializeField] private Upgradable<float> _cooldown = new(1f);
    [SerializeField, Min(1f)] private float _autoClickCooldown = 1.2f;
    [SerializeField, Min(0f)] private float _autoAimRange = 0f;

    private Camera _mainCamera;
    private bool _canAttack = true;
    private Coroutine _autoClick;
    private readonly Upgradable<AttackInfo> _attack = new(default);

    public Upgradable<int> BaseDamage => _baseDamage;

    public Upgradable<float> Cooldown => _cooldown;

    public Upgradable<AttackInfo> Attack => _attack;

    public event AttackAction OnAttack;

    private float GetSqrDistance(Ray ray, Vector3 position)
    {
        float volume = ray.direction.z * (ray.origin.x - position.x) - ray.direction.x * (ray.origin.z - position.z);
        float sqrSquare = ray.direction.x * ray.direction.x + ray.direction.z * ray.direction.z;
        return volume * volume / sqrSquare;
    }

    private IEnumerator AttackByRay(Ray ray)
    {
        if (!_canAttack)
            yield break;
        _canAttack = false;

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
            _attack.BaseValue = new()
            {
                Damage = _baseDamage.GetValue(),
                Target = aim,
                Position = hit?.point ?? aim.transform.position
            };

            AttackInfo info = _attack.GetValue();

            OnAttack?.Invoke(info);

            aim.TakeDamage(info.Damage);

            yield return new WaitForSeconds(_cooldown.GetValue());
        }

        _canAttack = true;
    }

    private IEnumerator AutoClick()
    {
        yield return new WaitUntil(() => _canAttack);
        while (true)
        {
            StartCoroutine(AttackByRay(_mainCamera.ScreenPointToRay(Input.mousePosition)));
            yield return new WaitForSeconds(_cooldown.GetValue() * _autoClickCooldown);
        }
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_autoClick == null && Input.GetKeyDown(Context.Settings.GetKey(EAction.Attack)))
        {
            _autoClick = StartCoroutine(AutoClick());
        }

        if (_autoClick != null && !Input.GetKey(Context.Settings.GetKey(EAction.Attack)))
        {
            StopCoroutine(_autoClick);
            _autoClick = null;
        }
    }
}