using System;
using UnityEngine;

public class BiomeStyleLoader : MonoBehaviour
{
    private const EGameLevel DefaultStyle = EGameLevel.OakForest;

    private void Start()
    {
        if (Context.Exist() && Context.Game.IsPlaying())
        {
            BiomeStyle style = Context.Game.Storage.GetResource<BiomeStyle>() ?? Context.Game.Storage.GetResource<BiomeStyle>(DefaultStyle);
            if (style == null)
                throw new ArgumentNullException(nameof(style));

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