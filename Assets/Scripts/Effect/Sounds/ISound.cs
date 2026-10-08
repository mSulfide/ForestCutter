using UnityEngine;

public interface ISound
{
    public float Volume { get; }
    public float Pitch { get; }
    public AudioClip Clip { get; }
}