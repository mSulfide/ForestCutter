using System;
using System.Collections.Generic;
using UnityEngine;

public class VictorySaver : MonoBehaviour, ISaver, IPathHierarchy
{
    [SerializeField] private UpgradeVictory _victory;
    [SerializeField] private GameSaver _saveParent;

    public void Save()
    {
        PathProvider provider = new(this);

        List<string> data = new(_victory.Progress);

        Context.Storage.Save(provider.GetPath(), data);
    }

    public void Load()
    {
        PathProvider provider = new(this);

        List<string> data = Context.Storage.Load<List<string>>(provider.GetPath());

        if (data != null)
            foreach (string upgrade in data)
                _victory.AddPoint(upgrade);
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

    string IPathHierarchy.Name => "Progress";
}