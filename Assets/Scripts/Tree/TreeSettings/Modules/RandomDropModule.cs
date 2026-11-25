using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomDropModule : ITreeModule
{
    [SerializeField] private List<RandomDrop> _drops;

    public IEnumerable<RandomDrop> Drop => _drops;
}