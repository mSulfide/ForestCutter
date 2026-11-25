using System;
using UnityEngine;

[Serializable]
public class ArmorModule : ITreeModule
{
    [SerializeField, Min(0f)] private float _armor = 0f;
    [SerializeField] private int _damageThreshold = 0;

    public float Armor => _armor;
    public int DamageThreshold => _damageThreshold;
}