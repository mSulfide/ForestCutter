using UnityEngine;

public class BiomeBackgroundColor : MonoBehaviour, ILevelDepended
{
    [SerializeField] private Camera _camera;

    public void OnLoad(EGameLevel level)
    {
        if (Context.Exist() && Context.Game.IsPlaying())
        {
            BiomeStyle[] styles = Resources.LoadAll<BiomeStyle>($"Biomes/{Context.Game.State.Level}/");

            if (styles.Length > 0)
                _camera.backgroundColor = styles[0].BackgroundColor;
        }
    }
}