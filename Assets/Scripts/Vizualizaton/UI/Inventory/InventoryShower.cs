using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryShower : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private ItemCounter _counterPrefab;

    private readonly Dictionary<Item, ItemCounter> _counters = new();

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

    private void UpdateInventory()
    {
        foreach (Item item in _inventory)
            if (_counters.TryGetValue(item, out ItemCounter counter))
                counter.SetValue(_inventory.CountOf(item));
            else
                AddCounter(item);
        List<Item> items = new(_counters.Keys);
        items.RemoveAll(item => _inventory.Contains(item));
        foreach (Item item in items)
            RemoveCounter(item);
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