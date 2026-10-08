using UnityEngine;

[RequireComponent(typeof(Health))]
public class TakeDamageEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private Sound _takeDamage;

    private ParticlePlayer _particlePlayer;
    private AudioPlayer _audioPlayer;
    private Health _health;

    public void AttackHandler(AttackInfo info)
    {
        if (info.Damage > 0)
        {
            Transform particle = _particlePlayer.Play(_particle).transform;
            particle.SetPositionAndRotation(info.Position, Quaternion.LookRotation(Vector3.up, info.Position - info.Target.transform.position));

            _audioPlayer.Play(_takeDamage);
        }
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        _particlePlayer = Context.GetParticlePlayer();
        _audioPlayer = Context.GetAudioPlayer();
    }

    private void OnEnable()
    {
        _health.OnAttacked += AttackHandler;
    }

    private void OnDisable()
    {
        _health.OnAttacked -= AttackHandler;
    }
}