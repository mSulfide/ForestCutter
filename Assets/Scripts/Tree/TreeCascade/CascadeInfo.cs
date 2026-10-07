using UnityEngine;

public struct CascadeInfo
{
    public float Force;
    public int Count;

    public override readonly string ToString() => $"{{ Force: {Force}, Count: {Count} }}";
}