using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ClickHandler()
    {
        if (Context.Game.IsPlaying())
            Context.Game.ExitGame();
        else
            Application.Quit();
    }
}