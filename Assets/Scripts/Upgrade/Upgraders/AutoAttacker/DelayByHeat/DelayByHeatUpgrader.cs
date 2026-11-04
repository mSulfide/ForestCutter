using UnityEngine;

public class DelayByHeatUpgrader : AutoAttackerUpgrader<AnimationCurve, DelayByHeatUpgradeList>
{
    private readonly SetUpgrade<AnimationCurve> _delayByHeat = new(null);

    public override void Invoke(AnimationCurve upgrade)
    {
        if (upgrade != null)
            _delayByHeat.Modificate(upgrade);
    }

    private void OnEnable()
    {
        AutoAttacker.DelayByHeat.Add(_delayByHeat);
    }

    private void OnDisable()
    {
        AutoAttacker.DelayByHeat.Remove(_delayByHeat);
    }
}