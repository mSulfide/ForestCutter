using System;
using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour, IAttackModifire
{
    [SerializeField] private float _recharge = 1f;

    private float _time = 0f;

    public float Recharge => _recharge;

    public event Action<float, Vector3> OnRecharge;

    public AttackInfo Modificate(AttackInfo info)
    {
        if (_time <= 0f)
        {
            info.Damage = 0;
            StartCoroutine(RechargeShield());
        }
        else
        {
            _time = _recharge;
        }
        OnRecharge?.Invoke(_recharge, info.Position);

        return info;
    }

    private IEnumerator RechargeShield()
    {
        _time = _recharge;
        while (_time > 0f)
        {
            _time -= Time.deltaTime;
            yield return null;
        }
    }
}