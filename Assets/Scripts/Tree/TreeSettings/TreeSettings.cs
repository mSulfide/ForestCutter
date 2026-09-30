using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tree", menuName = "ForestCutter/" + nameof(TreeSettings))]
public class TreeSettings : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _health = 3;
    [SerializeField] private List<RandomDrop> _drop = new();

    public GameObject Prefab => _prefab;
    public int Health => _health;
    public IEnumerable<RandomDrop> Drop => _drop;
}