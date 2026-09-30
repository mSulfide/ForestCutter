using System.Collections.Generic;
using UnityEngine;

public class PlayerSaver : MonoBehaviour, ISaver, IPathHierarchy
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameSaver _saveParent;

    public void Save()
    {
        PathProvider provider = new(this);

        PlayerData data = new()
        {
            Inventory = _inventory.GetData()
        };

        Context.Storage.Save(provider.GetPath(), data);
    }

    public void Load()
    {
        PathProvider provider = new(this);

        PlayerData data = Context.Storage.Load<PlayerData>(provider.GetPath());

        _inventory.SetData(data.Inventory);
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

    string IPathHierarchy.Name => "Player";
}