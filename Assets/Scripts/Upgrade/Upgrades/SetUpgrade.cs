public class SetUpgrade<T> : Upgrade<T>
{
    private T _value;

    public SetUpgrade(T value) : base(EUpgradePriority.Set)
    {
        _value = value;
    }

    public void SetValue(T value)
    {
        _value = value;
    }

    public override T Modificate(T value)
    {
        return _value ?? value;
    }
}