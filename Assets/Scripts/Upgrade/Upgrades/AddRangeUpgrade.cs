public class AddRangeUpgrade : Upgrade<float>
{
    private float _modifier;

    public AddRangeUpgrade(float modifier) : base(EUpgradePriority.Increment)
    {
        _modifier = modifier;
    }

    public void SetModifier(float modifier)
    {
        _modifier = modifier;
    }

    public override float Modificate(float value)
    {
        return value + _modifier;
    }
}