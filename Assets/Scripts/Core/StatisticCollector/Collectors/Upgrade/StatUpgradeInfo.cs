using System;
using System.Collections.Generic;

[Serializable]
public struct StatUpgradeInfo
{
    public string Name;
    public Dictionary<string, uint> Cost;
    public int Level;
}