using UnityEngine;

public class MyDebug : MonoBehaviour
{
    private Health _health;

    private void AttackHandler(AttackInfo info)
    {
        Debug.Log($"{info}");
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.OnAttacked += AttackHandler;
    }

    private void OnDisable()
    {
        _health.OnAttacked -= AttackHandler;
    }
}
