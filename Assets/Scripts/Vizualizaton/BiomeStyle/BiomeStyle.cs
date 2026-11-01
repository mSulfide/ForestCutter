using UnityEngine;

[CreateAssetMenu(fileName = "Style", menuName = "ForestCutter/Biomes/" + nameof(BiomeStyle))]
public class BiomeStyle : ScriptableObject
{
    [SerializeField] private Color _backgroundColor;
    [SerializeField] private Material _skybox;
    [SerializeField] private GameObject _islandPrefab;

    public Color BackgroundColor => _backgroundColor;
    public Material Skybox => _skybox;
    public GameObject IslandPrefab => _islandPrefab;
}