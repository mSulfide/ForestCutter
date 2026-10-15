using System.Collections.Generic;
using UnityEngine;

public class ShieldRechargeGenerator : MonoBehaviour
{
    [SerializeField] private TreeSpawner _spawner;
    [SerializeField] private ShieldRecharge _prefab;

    private readonly Queue<ShieldRecharge> _unbusyIndicators = new();

    private void SpawnHandler(GameObject tree)
    {
        if (tree.TryGetComponent(out Shield shield))
        {
            ShieldRecharge recharge = GetIndicator();

            recharge.Shield = shield;

            recharge.gameObject.SetActive(true);

            if (tree.TryGetComponent(out Health health))
            {
                health.OnPointsOver += () =>
                {
                    recharge.gameObject.SetActive(false);
                    _unbusyIndicators.Enqueue(recharge);
                };
            }
        }
    }

    private ShieldRecharge GetIndicator() => _unbusyIndicators.Count > 0 ? _unbusyIndicators.Dequeue() : Instantiate(_prefab, transform);

    private void OnEnable()
    {
        _spawner.OnSpawn += SpawnHandler;
    }

    private void OnDisable()
    {
        _spawner.OnSpawn -= SpawnHandler;
    }
}