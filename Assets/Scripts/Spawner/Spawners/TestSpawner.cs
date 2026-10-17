using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : TreeSpawner
{
    [SerializeField] private TestAttacker _attacker;

    private readonly Dictionary<GameObject, GameObject> _prefabToTree = new();

    public void Spawn(float time)
    {
        Dictionary<Item, uint> inventory = new();
        uint totalDamage = 0;
        uint totalClick = 0;
        float breakTime = 0f;

        while (time > 0)
        {
            time -= Delay.GetValue();

            TreeSettings tree = TreePool.GetValue();

            if (!_prefabToTree.ContainsKey(tree.Prefab))
            {
                GameObject treeObject = Instantiate(tree.Prefab);
                treeObject.GetComponent<Death>().enabled = false;
                treeObject.GetComponent<DeathEffect>().enabled = false;
                _prefabToTree.Add(tree.Prefab, treeObject);
            }

            Health health = _prefabToTree[tree.Prefab].GetComponent<Health>();
            health.SetMax(tree.Health, true);

            var info = _attacker.Kill(health);
            totalClick += info.ClickCount;
            breakTime += info.Time;

            if (health.IsAlive)
                continue;

            foreach (var drop in tree.Drop)
                if (inventory.ContainsKey(drop.ItemType))
                    inventory[drop.ItemType] += drop.Count;
                else
                    inventory.Add(drop.ItemType, drop.Count);

            totalDamage += (uint)tree.Health;
        }

        string answer = "";
        foreach (var pair in inventory)
            answer += $"{totalClick}\t{breakTime}\t{totalDamage}\t{pair.Key.name}\t{pair.Value}\n";

        GUIUtility.systemCopyBuffer = answer;
    }

    private void OnEnable() { }

    private void OnDisable() { }
}