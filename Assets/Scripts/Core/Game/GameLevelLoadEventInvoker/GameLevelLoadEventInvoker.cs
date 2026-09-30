using System.Linq;
using UnityEngine;

public class GameLevelLoadEventInvoker : MonoBehaviour
{
    private void Load()
    {
        EGameLevel level = Context.Game.State.Level;

        foreach (ILevelDepended depended in FindObjectsOfType<MonoBehaviour>(true).OfType<ILevelDepended>())
            depended.OnLoad(level);
    }

    private void Start()
    {
        Load();
    }
}