using System;
using System.Collections.Generic;

[Serializable]
public struct DropData
{
    public float X;
    public float Y;
    public float Z;
    public float YRotation;
    public Dictionary<string, uint> Inventory;
    public string Name;
}