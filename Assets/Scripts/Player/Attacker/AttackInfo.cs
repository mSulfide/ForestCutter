using UnityEngine;

public struct AttackInfo
{
    public int Damage;
    public Health Target;
    public Vector3 Position;
    public bool IsCrit;
    public float Cooldown;
    public float Force;
}