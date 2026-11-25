using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tree", menuName = "ForestCutter/" + nameof(TreeSettings))]
public class TreeSettings : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _health = 3;
    [SerializeReference, SubclassSelector] private List<ITreeModule> _modules = new();


    public GameObject Prefab => _prefab;
    public int Health => _health;
    public IEnumerable<ITreeModule> Modules => _modules;

    public T GetModule<T>() where T : class, ITreeModule
    {
        foreach (ITreeModule module in _modules)
            if (module is T result)
                return result;
        return null;
    }
}