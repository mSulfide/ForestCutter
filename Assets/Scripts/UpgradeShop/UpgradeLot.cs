using TMPro;
using UnityEngine;

public class UpgradeLot : MonoBehaviour
{
    public delegate void DealAction(DealInfo info);

    [SerializeField] private TextMeshProUGUI _text;

    private PriceList _costs;
    private Upgrader _upgrader;

    public event DealAction OnDeal;

    public void Init(PriceList costs, Upgrader upgrader)
    {
        _costs = costs;
        _upgrader = upgrader;

        _text.text = _costs.Message;
    }

    public Cost GetCost(int level) => 0 <= level && level < Mathf.Min(_costs.Count, _upgrader.MaxLevel + 1) ? _costs[level] : null;

    public Cost GetNextLevelCost() => GetCost(_upgrader.Level + 1);

    public void PayLevelUp(Inventory wallet)
    {
        Cost cost = GetNextLevelCost();
        if (IsEnoughMoney(wallet, cost))
        {
            foreach (ItemCostPair pair in cost)
                wallet.Remove(pair.Item, pair.Cost);
            _upgrader.LevelUp();

            DealInfo info = new()
            {
                Cost = cost,
                Level = _upgrader.Level
            };
            OnDeal?.Invoke(info);
        }
    }

    public void PayLevelUp() => PayLevelUp(transform.parent.GetComponent<UpgradeShop>().Wallet);

    private bool IsEnoughMoney(Inventory wallet, Cost cost)
    {
        if (cost == null)
            return false;
        foreach (ItemCostPair pair in cost)
            if (wallet.CountOf(pair.Item) < pair.Cost)
                return false;
        return true;
    }
}