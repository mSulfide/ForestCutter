using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PriceList", menuName = "ForestCutter/Shop/" + nameof(PriceList))]
public class PriceList : ScriptableObject
{
    [SerializeField] private UpgradeList _current;
    [SerializeField] private List<Cost> _costs = new();
    [SerializeField] private string _message;

    public UpgradeList Current => _current;

    public int Count => _costs.Count;

    public Cost this[int index] => _costs[index];

    public string Message => _message;
}