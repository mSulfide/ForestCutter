using System;
using UnityEngine;

[RequireComponent(typeof(TreeSpawner))]
public class BossSpawnEventListener : MonoBehaviour
{
    private TreeSpawner _spawner;

    public event Action<BossTree> OnBossSpawn;
    public event Action<BossTree> OnBossDespawn;

    private void SpawnHandler(GameObject tree)
    {
        if (tree.TryGetComponent(out BossTree boss))
            OnBossSpawn?.Invoke(boss);

    }

    private void DespawnHandler(GameObject tree)
    {
        if (tree.TryGetComponent(out BossTree boss))
            OnBossDespawn?.Invoke(boss);
    }

    private void Awake()
    {
        _spawner = GetComponent<TreeSpawner>();
    }

    private void OnEnable()
    {
        _spawner.OnSpawn += SpawnHandler;
        _spawner.OnDespawn += DespawnHandler;
    }

    private void OnDisable()
    {
        _spawner.OnSpawn -= SpawnHandler;
    }
}
