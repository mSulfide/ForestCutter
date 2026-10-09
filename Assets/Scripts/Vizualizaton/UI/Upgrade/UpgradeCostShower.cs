using System.Collections.Generic;
using UnityEngine;

public class UpgradeCostShower : MonoBehaviour
{
    [SerializeField] private UpgradeLot _upgradeLot;
    [SerializeField] private ItemCounter _counterPrefab;

    private readonly List<ItemCounter> _counters = new();

    private void UpdateCost()
    {
        foreach (ItemCounter counter in _counters)
            Destroy(counter.gameObject);
        _counters.Clear();

        Cost cost = _upgradeLot.GetNextLevelCost();
        if (cost != null)
        {
            foreach (ItemCostPair pair in cost)
            {
                ItemCounter counter = Instantiate(_counterPrefab, transform);
                _counters.Add(counter);

                counter.SetItem(pair.Item);
                counter.SetValue(pair.Cost);
            }
        }
    }

    private void DealHandler(DealInfo info)
    {
        UpdateCost();
    }

    private void Start()
    {
        UpdateCost();
    }

    private void OnEnable()
    {
        _upgradeLot.OnDeal += DealHandler;
    }

    private void OnDisable()
    {
        _upgradeLot.OnDeal -= DealHandler;
    }
}