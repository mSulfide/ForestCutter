using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class StatInventoryListener : MonoBehaviour
{
    private Inventory _inventory;

    private void ChangeHandler(Item item, int count)
    {
        StatInventoryChangeInfo info = new()
        {
            Item = item.name,
            Count = count
        };

        Context.Stats.AddRecord(info);
    }

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
    }

    private void OnEnable()
    {
        _inventory.OnChanged += ChangeHandler;
    }

    private void OnDisable()
    {
        _inventory.OnChanged -= ChangeHandler;
    }
}