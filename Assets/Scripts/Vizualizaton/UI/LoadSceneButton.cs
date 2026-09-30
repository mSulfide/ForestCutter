using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private EScene _scene;

    public void ClickHandler()
    {
        Context.Game.LoadScene(_scene);
    }
}