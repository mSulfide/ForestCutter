using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] private Inventory _wallet;
    [SerializeField] private UpgradeLot _lotPrefab;
    [SerializeField] private List<Upgrader> _upgraders = new();

    public Inventory Wallet => _wallet;

    private void CreateUpgradeLot(PriceList priceList, Upgrader upgrader)
    {
        UpgradeLot lot = Instantiate(_lotPrefab, transform);
        lot.name = priceList.name;
        lot.Init(priceList, upgrader);
    }

    private void InitUpgradeLot()
    {
        foreach (PriceList price in Context.Game.Storage.GetResources<PriceList>())
        {
            Upgrader upgrader = _upgraders.Find(x => x.UpgradeList == price.Current);

            if (upgrader != null)
                CreateUpgradeLot(price, upgrader);
        }
    }

    private void Start()
    {
        InitUpgradeLot();
    }
}