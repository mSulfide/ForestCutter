using UnityEngine;

public class ActiveInGameLevel : MonoBehaviour
{
    [SerializeField] private EGameLevel _level;

    private void Start()
    {
        gameObject.SetActive(Context.Game.State.Level == _level);
    }
}