using UnityEngine;

[DefaultExecutionOrder(-50)]
public class DefaultGameInitializer : MonoBehaviour
{
    [SerializeField] private string _name = "_Test";
    [SerializeField] private EGameLevel _startLevel;

    private void Start()
    {
        Context.Game.StartGame(_name, _startLevel);
    }
}