using UnityEngine;

[DefaultExecutionOrder((int)ESaverPriority.Game)]
public class GameSaver : MonoBehaviour, ISaver, IPathHierarchy
{
    public enum EResource
    {
        GameState,
        SaveData
    }

    public const string DefaultSaveName = "UnknownSave";

    private string _saveName = DefaultSaveName;

    public void Save()
    {
        PathProvider provider = new(this);

        SaveData(provider);
    }

    private void SaveData(PathProvider provider)
    {
        SaveData data = new()
        {
            Name = _saveName,
            Version = Application.version
        };
        Context.Storage.Save(provider.GetPath($"{EResource.SaveData}"), data);
    }

    private void Start()
    {
        if (Context.Exist() && Context.Game.IsPlaying())
            _saveName = Context.Game.State.Name;
    }

    IPathHierarchy IPathHierarchy.Parent => Context.Saves;

    string IPathHierarchy.Name => _saveName;
}