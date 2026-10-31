using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class AutoAttacker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _maxDelay;
    [SerializeField] private float _deltaDelay;

    private Attacker _attacker;
    private float _delay = 0f;
    private Coroutine _autoAttack;

    private float Delay
    {
        get
        {
            return _delay;
        }
        set
        {
            _delay = Mathf.Min(value, _maxDelay);
            _cooldown.SetModifier(_delay);
        }
    }

    private readonly CooldownUpgrade _cooldown = new(0f);

    public void Stop()
    {
        Delay = 0f;
        if (_autoAttack != null)
            StopCoroutine(_autoAttack);
    }

    private IEnumerator DelayedAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        _attacker.AttackByRay(_player.Pointer);
    }

    private void AttackHandler(AttackInfo info)
    {
        Delay += _deltaDelay;

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
        _attacker.Cooldown.Add(_cooldown);
    }

    private void OnDisable()
    {
        _attacker.OnAttack -= AttackHandler;
        _attacker.Cooldown.Remove(_cooldown);
    }
}