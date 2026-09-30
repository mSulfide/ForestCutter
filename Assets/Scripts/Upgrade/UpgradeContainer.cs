using System;
using System.Collections;
using System.Collections.Generic;

public class UpgradeContainer<T> : IEnumerable<Upgrade<T>>
{
    private readonly List<Upgrade<T>> _upgrades = new();

    public void Add(Upgrade<T> upgrade)
    {
        _upgrades.Add(upgrade);
        _upgrades.Sort((a, b) => a.Priority - b.Priority);
    }

    public IEnumerator<Upgrade<T>> GetEnumerator() => _upgrades.GetEnumerator();

    public void Remove(Upgrade<T> upgrade) => _upgrades.Remove(upgrade);

    public Upgrade<T> Find(Predicate<Upgrade<T>> match) => _upgrades.Find(match);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}