using RandMath;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerSettings", menuName = "ForestCutter/Biomes/" + nameof(TreeSpawnerSettings))]
public class TreeSpawnerSettings : ScriptableObject
{
    [SerializeField, Min(1)] private int _maxSpawnCount = 3;
    [SerializeField, Min(0.035f)] private float _delay = 2f;
    [SerializeField] private RandomValue<TreeSettings> _treePool;

    public RandomValue<TreeSettings> TreePool => _treePool;
    public int MaxSpawnCount => _maxSpawnCount;
    public float Delay => _delay;
}