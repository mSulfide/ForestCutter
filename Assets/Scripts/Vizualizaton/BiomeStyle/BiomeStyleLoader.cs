using UnityEngine;

public class BiomeStyleLoader : MonoBehaviour
{
    private void Start()
    {
        if (Context.Exist() && Context.Game.IsPlaying())
        {
            BiomeStyle style = Context.Game.Storage.GetResource<BiomeStyle>();

            if (style.Skybox != null)
            {
                RenderSettings.skybox = style.Skybox;
                DynamicGI.UpdateEnvironment();
            }

            if (style.IslandPrefab != null)
            {
                Instantiate(style.IslandPrefab, transform);
            }
        }
    }
}