public class IncrementUpgrade : Upgrade<int>
{
    private int _modifier;

    public IncrementUpgrade(int modifier) : base(EUpgradePriority.Increment)
    {
        _modifier = modifier;
    }

    public void SetModifier(int modifier)
    {
        _modifier = modifier;
    }

    public override int Modificate(int value)
    {
        return value + _modifier;
    }
}
