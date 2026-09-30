using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField, Min(1)] private int _maxSources = 3;

    private float _masterVolume = 0.2f;
    private readonly Queue<AudioSource> _sources = new();

    public void Play(Sound sound) => Play(sound.Clip, sound.Pitch, sound.Volume);

    public void Play(AudioClip sound, float pitch = 1f, float volume = 1f)
    {
        if (sound == null)
            return;
        AudioSource source = GetSource();
        source.clip = sound;
        source.pitch = pitch;
        source.volume = volume * _masterVolume;
        source.Play();
    }

    public void SetVolume(float volume)
    {
        _masterVolume = volume;
    }

    private void InstantiateSorces()
    {
        int k = 0;
        while (_sources.Count < _maxSources)
        {
            AudioSource source = new GameObject($"AudioSource{k++}").AddComponent<AudioSource>();
            source.transform.SetParent(transform);
            _sources.Enqueue(source);
        }
    }

    private AudioSource GetSource()
    {
        AudioSource source = _sources.Dequeue();
        _sources.Enqueue(source);
        return source;
    }

    private void Start()
    {
        InstantiateSorces();
    }
}
