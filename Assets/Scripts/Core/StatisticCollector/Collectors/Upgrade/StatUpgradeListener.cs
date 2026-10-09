using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UpgradeLot))]
public class StatUpgradeListener : MonoBehaviour
{
    private UpgradeLot _lot;

    private void DealHandler(DealInfo info)
    {
        Dictionary<string, uint> cost = new();
        foreach (var pair in info.Cost)
            cost.Add(pair.Item.name, pair.Cost);

        StatUpgradeInfo stat = new()
        {
            Name = _lot.name,
            Cost = cost,
            Level = info.Level
        };

        Context.Stats.AddRecord(stat);
    }

    private void Awake()
    {
        _lot = GetComponent<UpgradeLot>();
    }

    private void OnEnable()
    {
        _lot.OnDeal += DealHandler;
    }

    private void OnDisable()
    {
        _lot.OnDeal -= DealHandler;
    }
}