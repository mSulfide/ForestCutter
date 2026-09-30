using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cost : IEnumerable<ItemCostPair>
{
    [SerializeField] private List<ItemCostPair> _items;

    public IEnumerator<ItemCostPair> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}