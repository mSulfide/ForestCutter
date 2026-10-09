using System;
using System.Collections.Generic;
using UnityEngine;

public class StatisticCollector : MonoBehaviour, ISaver
{
    [Serializable]
    private class StatRecord
    {
        public string Type;
        public float Time;
        public object Data;

        public StatRecord(object data)
        {
            Type = data.GetType().ToString();
            Time = UnityEngine.Time.time;
            Data = data;
        }
    }

    private const string path = "Logs";

    private readonly List<StatRecord> _records = new();

    public void AddRecord(object record)
    {
        _records.Add(new(record));
    }

    public void Save()
    {
        PathProvider provider = new(Context.Saves);

        Context.Storage.Append(provider.GetPath(Context.Game.State.Name, path), _records);

        _records.Clear();
    }
}