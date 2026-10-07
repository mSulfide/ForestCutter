using UnityEngine;

public class TreeCascadeUpgrade : Upgrade<AttackInfo>
{
    private float _addForce = 0f;
    private int _addCount = 0;

    public TreeCascadeUpgrade(float addForce, int addCount) : base(EUpgradePriority.Set)
    {
        _addForce = addForce;
        _addCount = addCount;
    }

    public void SetForceModifire(float modifire)
    {
        _addForce = modifire;
    }

    public void SetCountModifire(int modifire)
    {
        _addCount = modifire;
    }

    public override AttackInfo Modificate(AttackInfo value)
    {
        CascadeInfo cascade = value.Cascade;

        cascade.Force += _addForce;
        cascade.Count += _addCount;

        value.Cascade = cascade;
        return value;
    }
}