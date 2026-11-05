using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeVictory : MonoBehaviour
{
    [SerializeField, Min(1)] private int _pointsToWin = 1;

    private readonly List<string> _points = new();

    public event Action OnWin;

    public IEnumerable<string> Progress => _points;

    public void AddPoint(string upgrade)
    {
        if (upgrade == null)
            return;

        if (!_points.Contains(upgrade))
        {
            _points.Add(upgrade);
            if (_points.Count >= _pointsToWin)
            {
                OnWin?.Invoke();
                Context.Game.WinGame();
            }
        }
    }
}