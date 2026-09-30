using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder((int)ESaverPriority.Upgrades)]
public class UpgraderSaver : MonoBehaviour, ISaver, IPathHierarchy
{
    [SerializeField] private List<Upgrader> _upgraders;
    [SerializeField] private GameSaver _saveParent;

    private Dictionary<string, int> _cash;

    public void Save()
    {
        PathProvider provider = new(this);

        Dictionary<string, int> data = _cash != null ? new(_cash) : new();
        foreach (Upgrader upgrader in _upgraders)
            if (data.ContainsKey(upgrader.UpgradeList.name))
                data[upgrader.UpgradeList.name] = upgrader.Level;
            else
                data.Add(upgrader.UpgradeList.name, upgrader.Level);

        Context.Storage.Save(provider.GetPath(), data);
    }

    public void Load()
    {
        PathProvider provider = new(this);

        Dictionary<string, int> data = Context.Storage.Load<Dictionary<string, int>>(provider.GetPath());
        if (data != null)
            foreach (var upgrade in data)
                _upgraders.Find(lot => lot.UpgradeList.name == upgrade.Key)?.SetLevel(upgrade.Value);

        _cash = data;
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

    string IPathHierarchy.Name => "Upgrades";
}