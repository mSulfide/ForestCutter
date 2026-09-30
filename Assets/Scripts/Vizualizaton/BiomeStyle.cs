using UnityEngine;

[CreateAssetMenu(fileName = "Style", menuName = "ForestCutter/Biomes/" + nameof(BiomeStyle))]
public class BiomeStyle : ScriptableObject
{
    [SerializeField] private Color _backgroundColor;

    public Color BackgroundColor => _backgroundColor;
}