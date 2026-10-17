public class InfinityInventory : Inventory
{
    private const uint count = 9999;

    public override uint CountOf(Item type)
    {
        return count;
    }
}