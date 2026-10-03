using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CursorRecharge : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Image _image;

    private Coroutine _fillCursore;

    private IEnumerator FillCursore(float cooldown)
    {
        _image.fillAmount = 0f;

        float time = 0f;
        while (time <= cooldown)
        {
            time += Time.deltaTime;
            _image.fillAmount = Mathf.Clamp(time / cooldown, 0f, 1f);
            yield return null;
        }

        _image.fillAmount = 0f;
    }

    private void AttackHandler(AttackInfo info)
    {
        if (_fillCursore != null)
            StopCoroutine(_fillCursore);
        _fillCursore = StartCoroutine(FillCursore(info.Cooldown));
    }

    private void OnEnable()
    {
        _attacker.OnAttackAction += AttackHandler;
    }

    private void OnDisable()
    {
        _attacker.OnAttackAction -= AttackHandler;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}