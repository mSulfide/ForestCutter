using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class AutoAttacker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Upgradable<AnimationCurve> _delayByHeat;
    [Header("Overheating")]
    [SerializeField] private AnimationCurve _heatingByHeat;
    [SerializeField] private Upgradable<float> _waitColding = new(0f);
    [SerializeField] private Upgradable<float> _workColding = new(0f);

    private Attacker _attacker;
    private float _overheat = 0f;
    private Coroutine _autoAttack;

    private readonly MultiplierUpgrade _cooldown = new(1f);

    public Upgradable<AnimationCurve> DelayByHeat => _delayByHeat;

    public Upgradable<float> WaitColding => _waitColding;

    public Upgradable<float> WorkColding => _workColding;

    public float DelayMultiplier => _delayByHeat.GetValue().Evaluate(_overheat);

    public float Overheat => _overheat;

    public bool IsOverheating { get; private set; }

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
        _autoAttack = null;
    }

    private IEnumerator DelayedAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return TryAttack();
    }

    private IEnumerator TryAttack()
    {
        yield return new WaitUntil(() => _attacker.TryAttackByRay(_player.Pointer));
        float deltaHeat = _heatingByHeat.Evaluate(_overheat);
        if (_overheat + deltaHeat < 1f)
        {
            _overheat += deltaHeat;
        }
        else
        {
            _overheat = 1f;
            IsOverheating = true;
        }
    }

    private void AttackHandler(AttackInfo info)
    {
        if (IsOverheating)
            return;

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

    private void Update()
    {
        float deltaHeat = (_autoAttack == null ? _waitColding : _workColding).GetValue() * Time.deltaTime;
        if (_overheat - deltaHeat > 0f)
        {
            _overheat -= deltaHeat;
        }
        else
        {
            _overheat = 0f;
            IsOverheating = false;
        }
        _cooldown.SetModifier(IsOverheating ? 1f : DelayMultiplier);
    }
}