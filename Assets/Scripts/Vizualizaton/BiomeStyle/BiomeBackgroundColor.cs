using UnityEngine;

public class BiomeBackgroundColor : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Start()
    {
        if (Context.Exist() && Context.Game.IsPlaying())
        {
            BiomeStyle style = Context.Game.Storage.GetResource<BiomeStyle>();

            _camera.backgroundColor = style.BackgroundColor;
        }
    }
}