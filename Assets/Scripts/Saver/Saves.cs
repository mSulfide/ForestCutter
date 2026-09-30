using System;
using System.Collections.Generic;
using UnityEngine;

public class Saves : IPathHierarchy
{
    public IEnumerable<SaveData> GetAllSaves()
    {
        PathProvider provider = new(this);
        foreach (string a in Context.Storage.GetDirectories(provider.GetPath()))
        {
            SaveData? save = Context.Storage.Load<SaveData?>(provider.GetPath(a, $"{GameSaver.EResource.SaveData}"));
            if (save != null)
                yield return save.Value;
        }
    }

    internal void DeleteSave(string text)
    {
        PathProvider provider = new(this);
        Context.Storage.Delete(provider.GetPath(text), EFileType.Directory);
    }

    IPathHierarchy IPathHierarchy.Parent => null;

    string IPathHierarchy.Name => nameof(Saves);
}