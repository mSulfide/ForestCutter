using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TreeData
{
    public float X;
    public float Y;
    public float Z;
    public Dictionary<string, uint> Inventory;
    public int Health;
    public string Name;
}