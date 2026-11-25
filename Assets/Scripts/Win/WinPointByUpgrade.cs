using System;
using System.Collections.Generic;
using UnityEngine;

public class WinPointByUpgrade : MonoBehaviour
{
    [SerializeField] private UpgradeVictory _victory;
    [SerializeField] private Upgrader _upgrader;
    [SerializeField] private int _level = 0;

    private void LevelUpHandler(int level)
    {
        if (_upgrader?.UpgradeList == null)
            throw new ArgumentNullException(nameof(_upgrader), "UpgradeList does not exist");
        if (level == _level)
            _victory.AddPoint(_upgrader.UpgradeList.name);
    }

    private void OnEnable()
    {
        _upgrader.OnLevelUp += LevelUpHandler;
    }

    private void OnDisable()
    {
        _upgrader.OnLevelUp -= LevelUpHandler;
    }
}