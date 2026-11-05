using TMPro;
using UnityEngine;

public class StatShower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI _click;

    private void Start()
    {
        GlobalStatistic stats = Context.Stats.Global;

        int time = (int)stats.Time;
        _time.text = $"{time / 3600}:{time / 60 % 60:D2}:{time % 60:D2}";

        _damage.text = stats.TotalDamage.ToString();
        _click.text = stats.TotalClick.ToString();
    }
}