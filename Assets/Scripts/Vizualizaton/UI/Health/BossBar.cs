using UnityEngine;

public class BossBar : MonoBehaviour
{
    [SerializeField] private BossSpawnEventListener _listener;
    [SerializeField] private HealthBar _bar;

    private void BossSpawnHandler(BossTree boss)
    {
        _bar.Initialize(boss.GetComponent<Health>());
        _bar.gameObject.SetActive(true);
    }

    private void BossDespawnHandler(BossTree boss)
    {
        _bar.gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        _listener.OnBossSpawn += BossSpawnHandler;
        _listener.OnBossDespawn += BossDespawnHandler;
    }

    private void OnDisable()
    {
        _listener.OnBossSpawn -= BossSpawnHandler;
        _listener.OnBossDespawn -= BossDespawnHandler;
    }
}
