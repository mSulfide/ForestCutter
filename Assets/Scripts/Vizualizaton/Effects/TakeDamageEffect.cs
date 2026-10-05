using UnityEngine;

[RequireComponent(typeof(Health))]
public class TakeDamageEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private ParticlePlayer _particlePlayer;
    private Health _health;

    public void AttackHandler(AttackInfo info)
    {
        Transform particle = _particlePlayer.Play(_particle).transform;
        particle.SetPositionAndRotation(info.Position, Quaternion.LookRotation(Vector3.up, info.Position - info.Target.transform.position));
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        _particlePlayer = Context.GetParticlePlayer();
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