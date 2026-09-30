using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ForestCutter/" + nameof(Item))]
public class Item : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private Drop _dropPrefab;

    public Sprite Icon => _icon;
    public Drop DropPrefab => _dropPrefab;
}