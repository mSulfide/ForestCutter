using System;
using System.Collections;
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
    private Coroutine _timer;

    public GlobalStatistic Global => _global;

    public void AddRecord(object record, float time)
    {
        _records.Add(new(record, time));
    }

    public void AddRecord(object record) => AddRecord(record, _global.Time);

    public void Save()
    {
        PathProvider provider = new(Context.Saves);

        Context.Storage.Append(provider.GetPath(Context.Game.State.Name, path), _records);

        _records.Clear();

        Context.Storage.Save(provider.GetPath(Context.Game.State.Name, pathGlobal), _global);
    }

    private void GameStartHandler()
    {
        PathProvider provider = new(Context.Saves);

        _global = Context.Storage.Load<GlobalStatistic>(provider.GetPath(Context.Game.State.Name, pathGlobal)) ?? new();

        _timer = StartCoroutine(StartTimer());
    }

    private void GameStopHandler()
    {
        StopCoroutine(_timer);
    }

    private IEnumerator StartTimer()
    {
        while (Context.Game.IsPlaying())
        {
            _global.Time += Time.deltaTime;
            yield return null;
        }
    }

    private void OnEnable()
    {
        Context.Game.OnStart += GameStartHandler;
        Context.Game.OnStop += GameStopHandler;
    }

    private void OnDisable()
    {
        Context.Game.OnStart -= GameStartHandler;
        Context.Game.OnStop -= GameStopHandler;
    }
}