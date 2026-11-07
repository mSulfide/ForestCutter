using UnityEngine;

public class AutoAttackerUpgrader : Upgrader<AnimationCurve, DelayByHeatUpgradeList>
{
    [SerializeField] private AutoAttacker _autoAttacker;

    private readonly SetUpgrade<AnimationCurve> _delayByHeat = new(null);

    public override void Invoke(AnimationCurve upgrade)
    {
        if (upgrade != null)
        {
            _autoAttacker.enabled = true;

            _delayByHeat.SetValue(upgrade);
        }
    }

    private void OnEnable()
    {
        _autoAttacker.DelayByHeat.Add(_delayByHeat);
    }

    private void OnDisable()
    {
        _autoAttacker.DelayByHeat.Remove(_delayByHeat);
    }
}