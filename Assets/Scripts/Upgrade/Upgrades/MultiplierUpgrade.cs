public class MultiplierUpgrade : Upgrade<float>
{
    private float _modifier;

    public MultiplierUpgrade(float modifier) : base(EUpgradePriority.Multiplier)
    {
        _modifier = modifier;
    }

    public void SetModifier(float modifier)
    {
        _modifier = modifier;
    }

    public override float Modificate(float value)
    {
        return value * _modifier;
    }
}