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

    public T GetResource<T>() where T : ScriptableObject => Resources.LoadAll<T>("Player").Concat(Resources.LoadAll<T>($"Biomes/{_state.Level}")).FirstOrDefault();
}