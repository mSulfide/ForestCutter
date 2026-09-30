public class Upgrade<T>
{
    public int Priority { get; private set; }
    
    public Upgrade(int priority)
    {
        Priority = priority;
    }

    public Upgrade(EUpgradePriority priority)
    {
        Priority = (int)priority;
    }

    public virtual T Modificate(T value) => value;
}