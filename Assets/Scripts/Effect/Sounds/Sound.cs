using RandMath;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Sound), menuName = "ForestCutter/Sounds/" + nameof(Sound))]
public class Sound : ScriptableObject, ISound
{
    [SerializeField] private float _volume = 1f;
    [SerializeField] private Range _pitch;
    [SerializeField] private AudioClip _clip;

    public float Volume => _volume;
    public float Pitch => _pitch;
    public AudioClip Clip => _clip;
}
