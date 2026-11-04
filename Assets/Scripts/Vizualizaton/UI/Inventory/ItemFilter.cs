using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ItemFilter), menuName = "ForestCutter/Inventory/" + nameof(ItemFilter))]
public class ItemFilter : ScriptableObject
{
    [SerializeField] private List<Item> _filter;

    public List<Item> Content => _filter;
}