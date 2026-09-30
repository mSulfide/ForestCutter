using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "ForestCutter/Biomes/" + nameof(BiomeSettings))]
public class BiomeSettings : ScriptableObject
{
    [SerializeField] private TreeSpawnerSettings _spawner;

    public TreeSpawnerSettings Spawner => _spawner;
}