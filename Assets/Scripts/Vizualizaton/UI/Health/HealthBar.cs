using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _current;

    private Health _health;

    public void Initialize(Health health)
    {
        _health = health;
        _current.fillAmount = (float)_health.Current / _health.Max;
    }

    private void HealthChangedHandler(int current, int max)
    {
        _current.fillAmount = (float)current / max;
    }

    private void OnEnable()
    {
        _health.OnChanged += HealthChangedHandler;
    }

    private void OnDisable()
    {
        _health.OnChanged -= HealthChangedHandler;
    }
}