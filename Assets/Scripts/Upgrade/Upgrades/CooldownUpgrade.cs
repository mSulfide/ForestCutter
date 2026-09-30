using UnityEngine;

public class CooldownUpgrade : Upgrade<float>
{
    private float _modifire;

    public CooldownUpgrade(float modifire) : base(EUpgradePriority.Increment)
    {
        _modifire = modifire;
    }

    public void SetModifier(float modifire)
    {
        _modifire = modifire;
    }

    public override float Modificate(float value)
    {
        const float minDelay = 0.1f;

        return Mathf.Max(value - _modifire, minDelay);
    }
}