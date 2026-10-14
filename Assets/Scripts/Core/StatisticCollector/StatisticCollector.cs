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

        public StatRecord(object data, float time)
        {
            Type = data.GetType().ToString();
            Time = time;
            Data = data;
        }
    }

    private const string path = "Logs";
    private const string pathGlobal = "Global";

    private readonly List<StatRecord> _records = new();
    private GlobalStatistic _global;

    public GlobalStatistic Global => _global;

    public void AddRecord(object record)
    {
        _records.Add(new(record, _global.Time));
    }

    public void Save()
    {
        PathProvider provider = new(Context.Saves);

        Context.Storage.Append(provider.GetPath(Context.Game.State.Name, path), _records);

        _records.Clear();

        Context.Storage.Save(provider.GetPath(Context.Game.State.Name, pathGlobal), _global);
    }

    private void Start()
    {
        PathProvider provider = new(Context.Saves);

        _global = Context.Storage.Load<GlobalStatistic>(provider.GetPath(Context.Game.State.Name, pathGlobal)) ?? new();
    }

    private void Update()
    {
        _global.Time += Time.deltaTime;
    }
}