using RandMath;
using System;
using UnityEngine;

[Serializable]
public class RandomDrop
{
    [SerializeField] private Item _itemType;
    [SerializeField] private RandomValue<uint> _count;

    public Item ItemType => _itemType;
    public uint Count => _count;
}