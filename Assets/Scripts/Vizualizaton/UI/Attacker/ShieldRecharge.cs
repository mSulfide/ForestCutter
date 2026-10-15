using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShieldRecharge : MonoBehaviour
{
    [SerializeField] private Image _image;

    private Coroutine _recharging;
    private Shield _shield;

    public Shield Shield
    {
        set => _shield = value;
    }

    private void RechargeHandler(float time, Vector3 position)
    {
        transform.position = Camera.main.WorldToScreenPoint(position);

        if (_recharging != null)
            StopCoroutine(_recharging);
        _recharging = StartCoroutine(Recharging(time));
    }

    private IEnumerator Recharging(float rechargeTime)
    {
        _image.fillAmount = 0f;

        float time = 0f;
        while (time <= rechargeTime)
        {
            time += Time.deltaTime;
            _image.fillAmount = Mathf.Clamp(time / rechargeTime, 0f, 1f);
            yield return null;
        }

        _image.fillAmount = 0f;
    }

    private void OnEnable()
    {
        if (_shield != null)
            _shield.OnRecharge += RechargeHandler;
    }

    private void OnDisable()
    {
        if (_shield != null)
            _shield.OnRecharge -= RechargeHandler;
    }
}