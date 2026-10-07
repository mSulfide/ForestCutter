using UnityEngine;

public struct AttackInfo
{
    public int Damage;
    public Health Target;
    public Vector3 Position;
    public bool IsCrit;
    public float Cooldown;
    public float Force;
    public float ArmorIgnore;
    public CascadeInfo Cascade;

    public override readonly string ToString()
    {
        return $"{{ Damage: {Damage}, Target: {Target?.Current}, Position: {Position}, IsCrit: {IsCrit}, Cooldown: {Cooldown}, Force: {Force}, ArmorIgnore: {ArmorIgnore}, Cascade: {Cascade} }}";
    }
}