using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class InventorySaverExtension
{
    public static Dictionary<string, uint> GetData(this Inventory inventory)
    {
        Dictionary<string, uint> inventoryData = new();
        foreach (Item item in inventory)
            inventoryData.Add(item.name, inventory.CountOf(item));
        return inventoryData;
    }

    public static void SetData(this Inventory inventory, Dictionary<string, uint> data)
    {
        if (data != null)
        {
            inventory.Clear();
            foreach (var slot in data)
                inventory.Add(Resources.Load<Item>(Path.Combine("Items", slot.Key)), slot.Value);
        }
    }
}