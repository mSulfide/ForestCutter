using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : TreeSpawner
{
    public void Spawn(float time)
    {
        Dictionary<Item, uint> inventory = new();
        uint totalDamage = 0;

        while (time > 0)
        {
            TreeSettings tree = TreePool.GetValue();

            foreach (var drop in tree.Drop)
                if (inventory.ContainsKey(drop.ItemType))
                    inventory[drop.ItemType] += drop.Count;
                else
                    inventory.Add(drop.ItemType, drop.Count);

            totalDamage += (uint)tree.Health;

            time -= Delay.GetValue();
        }

        string answer = "";
        foreach (var pair in inventory)
            answer += $"{totalDamage}\t{pair.Key.name}\t{pair.Value}\n";

        GUIUtility.systemCopyBuffer = answer;
    }

    private void OnEnable() { }

    private void OnDisable() { }
}