using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStorage
{
    private GameState _state;

    public void SetState(GameState state)
    {
        _state = state;
    }

    public T GetUpgradeList<T>() where T : UpgradeList
    {
        return GetResource<T>();
    }

    public T GetResource<T>(EGameLevel biome) where T : ScriptableObject => GetResources<T>(biome).FirstOrDefault();

    public T GetResource<T>() where T : ScriptableObject => GetResources<T>().FirstOrDefault();

    public IEnumerable<T> GetResources<T>(EGameLevel biome) where T : ScriptableObject => Resources.LoadAll<T>("Player").Concat(Resources.LoadAll<T>($"Biomes/{biome}"));

    public IEnumerable<T> GetResources<T>() where T : ScriptableObject => GetResources<T>(_state.Level);
}