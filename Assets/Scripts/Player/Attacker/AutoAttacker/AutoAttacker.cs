using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class AutoAttacker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AnimationCurve _damageBySpeed;
    [SerializeField] private Upgradable<AnimationCurve> _delayByAttack;

    private Attacker _attacker;
    private uint _attackCount;
    private Coroutine _autoAttack;

    private readonly AutoAttackUpgrade _upgrade = new(1f, 1f);

    public Upgradable<AnimationCurve> DelayByHeat => _delayByAttack;

    public void StartAttack()
    {
        if (!enabled)
            return;

        if (_autoAttack != null)
            StopCoroutine(_autoAttack);
        _autoAttack = StartCoroutine(TryAttack());
    }

    public void StopAttack()
    {
        if (!enabled)
            return;

        if (_autoAttack != null)
            StopCoroutine(_autoAttack);
        DropAutoAttack();
    }

    private void DropAutoAttack()
    {
        _autoAttack = null;
        _attackCount = 0;
        Modificate();
    }

    private void Modificate()
    {
        float cooldown = _delayByAttack.GetValue().Evaluate(_attackCount);
        _upgrade.SetCooldown(cooldown);
        _upgrade.SetDamage(cooldown * _damageBySpeed.Evaluate(cooldown));
    }

    private IEnumerator DelayedAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return TryAttack();
    }

    private IEnumerator TryAttack()
    {
        yield return new WaitUntil(() => _attacker.TryAttackByRay(_player.Pointer));
        _attackCount++;
        Modificate();
    }

    private void AttackHandler(AttackInfo info)
    {
        if (_autoAttack != null)
            StopCoroutine(_autoAttack);
        _autoAttack = StartCoroutine(DelayedAttack(info.Cooldown));
    }

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        _attacker.OnAttack += AttackHandler;
        _attacker.Attack.Add(_upgrade);
    }

    private void OnDisable()
    {
        _attacker.OnAttack -= AttackHandler;
        _attacker.Attack.Remove(_upgrade);
    }
}