using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopShower : MonoBehaviour
{
    [SerializeField] private List<UpgradeLot> _lots = new();
    [SerializeField] private UpgradeLotShower _prefab;

    private List<UpgradeLotShower> _showers = new();

    private void UpdateLots()
    {
        _showers.Clear();

        foreach (UpgradeLot lot in _lots)
        {
            UpgradeLotShower shower = Instantiate(_prefab, transform);

            _showers.Add(shower);
        }
    }

    private void Start()
    {
        UpdateLots();
    }
}