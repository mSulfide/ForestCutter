using System.Collections;
using UnityEngine;

public class VictoryBanner : MonoBehaviour
{
    [SerializeField] private RectTransform _banner;
    [SerializeField] private UpgradeVictory _victory;

    private void WinHandler()
    {
        if (!Context.Game.State.Victory)
            _banner.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _victory.OnWin += WinHandler;
    }

    private void OnDisable()
    {
        _victory.OnWin -= WinHandler;
    }
}