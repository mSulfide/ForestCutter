using UnityEngine;

public class ActiveInGameLevel : MonoBehaviour, ILevelDepended
{
    [SerializeField] private EGameLevel _level;

    public void OnLoad(EGameLevel level)
    {
        gameObject.SetActive(level == _level);
    }
}