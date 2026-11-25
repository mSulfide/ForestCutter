using System;
using UnityEngine;

[Serializable]
public class ViscosityModule : ITreeModule
{
    [SerializeField, Min(0f)] private float _cooldownModifire = 0f;

    public float CooldownModifire => _cooldownModifire;
}