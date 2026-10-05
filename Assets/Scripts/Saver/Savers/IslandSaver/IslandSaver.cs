using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[DefaultExecutionOrder((int)ESaverPriority.Island)]
public class IslandSaver : MonoBehaviour, ISaver, IPathHierarchy
{
    public enum EResource
    {
        Drop,
        Trees
    }

    private const string IslandsPath = "Islands";

    [SerializeField] private GameSaver _saveParent;
    [SerializeField] private TreeSpawner _spawner;

    public void Save()
    {
        PathProvider provider = new(this);

        SaveTrees(provider, _spawner.GetComponentsInChildren<Health>().Select(health => health.transform));
        SaveDrop(provider, FindObjectsByType<Drop>().Select(drop => drop.transform));
    }

    public void Load()
    {
        PathProvider provider = new(this);

        LoadTrees(provider, _spawner);
        LoadDrop(provider);
    }

    private void SaveDrop(PathProvider provider, IEnumerable<Transform> drops)
    {
        List<DropData> data = new();
        foreach (Transform drop in drops)
            data.Add(new()
            {
                Name = drop.gameObject.name,
                X = drop.position.x,
                Z = drop.position.z,
                Inventory = drop.TryGetComponent(out Inventory inventory) ? inventory.GetData() : null
            });
        Context.Storage.Save(provider.GetPath($"{EResource.Drop}"), data);
    }

    public void LoadDrop(PathProvider provider)
    {
        List<DropData> data = Context.Storage.Load<List<DropData>>(provider.GetPath($"{EResource.Drop}"));
        if (data != null)
            foreach (DropData dropData in data)
            {
                Item item = Resources.Load<Item>(Path.Combine("Items", dropData.Name));

                GameObject drop = Instantiate(item.DropPrefab).gameObject;
                drop.name = item.name;

                drop.transform.position = new(dropData.X, 0f, dropData.Z);
                (drop.TryGetComponent(out Inventory inventory) ? inventory : drop.AddComponent<Inventory>()).SetData(dropData.Inventory);
            }
    }

    private void SaveTrees(PathProvider provider, IEnumerable<Transform> trees)
    {
        List<TreeData> data = new();
        foreach (Transform tree in trees)
            data.Add(new()
            {
                Name = tree.gameObject.name,
                X = tree.position.x,
                Y = tree.position.y,
                Z = tree.position.z,
                Health = tree.TryGetComponent(out Health health) ? health.Current : 0,
                Inventory = tree.TryGetComponent(out Inventory inventory) ? inventory.GetData() : null
            });
        Context.Storage.Save(provider.GetPath($"{EResource.Trees}"), data);
    }

    private void LoadTrees(PathProvider provider, TreeSpawner spawner)
    {
        List<TreeData> data = Context.Storage.Load<List<TreeData>>(provider.GetPath($"{EResource.Trees}"));
        if (data != null)
            foreach (TreeData treeData in data)
            {
                Vector3 position = new(treeData.X, treeData.Y, treeData.Z);
                TreeSettings settings = Resources.Load<TreeSettings>(Path.Combine("Biomes", $"{Context.Game.State.Level}","Trees", treeData.Name));
                GameObject tree = spawner.Spawn(settings, position);

                tree.GetComponent<Health>().SetMax(treeData.Health);

                tree.GetComponent<Inventory>().SetData(treeData.Inventory);
            }
    }

    private void Start()
    {
        if (Context.Exist() && Context.Storage != null)
        {
            Load();
        }
        else
        {
            enabled = false;
        }
    }

    IPathHierarchy IPathHierarchy.Parent => _saveParent;

    string IPathHierarchy.Name => PathProvider.Combine(IslandsPath, $"{Context.Game.State.Level}");
}