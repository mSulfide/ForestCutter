using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour, IEnumerable<Item>
{
    private readonly Dictionary<Item, uint> _inventory = new();

    public event Action OnChanged;

    public void Add(Item type, uint count = 1)
    {
        if (_inventory.ContainsKey(type))
            _inventory[type] += count;
        else
            _inventory.Add(type, count);
        if (count > 0)
            OnChanged?.Invoke();
    }

    public void Remove(Item type, uint count = uint.MaxValue)
    {
        if (_inventory.TryGetValue(type, out uint currentCount))
        {
            if (count < currentCount)
                _inventory[type] -= count;
            else
                _inventory.Remove(type);
            if (count > 0)
                OnChanged?.Invoke();
        }
    }

    public uint CountOf(Item type) => _inventory.TryGetValue(type, out uint count) ? count : 0;

    public void Clear()
    {
        _inventory.Clear();
        OnChanged?.Invoke();
    }

    public IEnumerator<Item> GetEnumerator()
    {
        foreach (var item in _inventory.Keys.ToList())
            yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
