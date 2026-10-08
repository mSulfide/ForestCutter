using RandMath;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(RandomSound), menuName = "ForestCutter/Sounds/" + nameof(RandomSound))]
public class RandomSound : ScriptableObject, ISound
{
    [SerializeField] private float _volume = 1f;
    [SerializeField] private Range _pitch;
    [SerializeField] private RandomValue<AudioClip> _clip;

    public float Volume => _volume;
    public float Pitch => _pitch;
    public AudioClip Clip => _clip;
}