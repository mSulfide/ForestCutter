using UnityEngine;

public class TreeGenerator
{
    public GameObject Spawn(TreeSettings settings)
    {
        GameObject original = Object.Instantiate(settings.Prefab);
        original.name = settings.name;

        Inventory inventory = original.TryGetComponent(out Inventory currentInventory) ? currentInventory : original.AddComponent<Inventory>();
        foreach (var item in settings.Drop)
            inventory.Add(item.ItemType, item.Count);

        Health health = original.TryGetComponent(out Health currentHealth) ? currentHealth : original.AddComponent<Health>();
        health.SetMax(settings.Health, true);

        return original;
    }
}
