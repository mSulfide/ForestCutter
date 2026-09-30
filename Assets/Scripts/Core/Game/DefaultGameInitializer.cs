using UnityEngine;

[DefaultExecutionOrder(-50)]
public class DefaultGameInitializer : MonoBehaviour
{
    [SerializeField] private EGameLevel _startLevel;

    private void Start()
    {
        Context.Game.StartGame("_Test", _startLevel);
    }
}