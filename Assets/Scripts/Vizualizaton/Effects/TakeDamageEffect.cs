using UnityEngine;

public class TakeDamageEffect : MonoBehaviour, IAttackEventListener
{
    [SerializeField] private ParticleSystem _particle;

    private ParticlePlayer _particlePlayer;

    public void AttackHandler(AttackInfo info)
    {
        Transform particle = _particlePlayer.Play(_particle).transform;
        particle.SetPositionAndRotation(info.Position, Quaternion.LookRotation(Vector3.up, info.Position - info.Target.transform.position));
    }

    private void Start()
    {
        _particlePlayer = FindObjectOfType<ParticlePlayer>();
    }
}