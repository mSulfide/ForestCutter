using UnityEngine;

public class ArmorIgnoreUpgrade : Upgrade<AttackInfo>
{
    private float _modifire;

    public ArmorIgnoreUpgrade(float modifire) : base(EUpgradePriority.Increment)
    {
        _modifire = modifire;
    }

    public void SetModifire(float modifire)
    {
        _modifire = Mathf.Max(0f, modifire);
    }

    public override AttackInfo Modificate(AttackInfo value)
    {
        value.ArmorIgnore += _modifire;
        
        return value;
    }
}