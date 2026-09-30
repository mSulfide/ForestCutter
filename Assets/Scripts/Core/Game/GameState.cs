using System;
using UnityEngine;

[Serializable]
public class GameState
{
    public const string DefaultName = "UnknownSave";

    private string _name = DefaultName;
    private EGameLevel _level = EGameLevel.OakForest;

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public EGameLevel Level
    {
        get => _level;
        set => _level = value;
    }
}