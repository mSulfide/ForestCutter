using UnityEngine;

public class SelectIsland : MonoBehaviour
{
    [SerializeField] private EGameLevel _level;

    public EGameLevel Level => _level;

    private void OnMouseUp()
    {
        if (Context.Game.State.Level == _level)
            Context.Game.LoadScene(EScene.MainScene);
        else
            Context.Game.LoadLevel(_level);
    }
}