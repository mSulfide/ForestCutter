using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    public ParticleSystem Play(ParticleSystem particle)
    {
        if (particle == null)
            return null;

        ParticleSystem source = Instantiate(particle, transform);
        source.Play();
        Destroy(source.gameObject, source.main.duration + source.main.startLifetime.constantMax);

        return source;
    }
}
