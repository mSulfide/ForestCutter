using System;
using UnityEngine;

[Serializable]
public class ShieldModule : ITreeModule
{
    [SerializeField, Min(0f)] private float _reacharge = 1f;

    public float Recharge => _reacharge;
}