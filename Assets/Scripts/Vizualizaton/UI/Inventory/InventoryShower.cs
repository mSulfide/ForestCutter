using System.Collections.Generic;
using UnityEngine;

public class InventoryShower : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private ItemCounter _counterPrefab;

    private readonly Dictionary<Item, ItemCounter> _counters = new();
    private List<Item> _filter;

    private void AddCounter(Item item)
    {
        ItemCounter counter = Instantiate(_counterPrefab, transform);
        counter.name = $"{item.name}Counter";
        counter.SetItem(item);
        counter.SetValue(_inventory.CountOf(item));
        _counters.Add(item, counter);
    }

    private void RemoveCounter(Item item)
    {
        if (_counters.TryGetValue(item, out ItemCounter counter))
        {
            _counters.Remove(item);
            Destroy(counter.gameObject);
        }
    }

    private void UpdateInventory(Item item, int count)
    {
        if (_filter != null && !_filter.Contains(item))
            return;

        if (_counters.TryGetValue(item, out ItemCounter counter))
            counter.SetValue(_inventory.CountOf(item));
        else
            AddCounter(item);

        if (_inventory.CountOf(item) <= 0)
            RemoveCounter(item);
    }

    private void SetBiomeFilter()
    {
        _filter = Context.Game.Storage.GetResource<ItemFilter>()?.Content;
    }

    private void Start()
    {
        if (Context.Exist() && Context.Game.IsPlaying())
            SetBiomeFilter();
    }

    private void OnEnable()
    {
        _inventory.OnChanged += UpdateInventory;
    }

    private void OnDisable()
    {
        _inventory.OnChanged -= UpdateInventory;
    }
}