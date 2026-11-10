using UnityEngine;

[RequireComponent(typeof(Health))]
public class DeathEffect : MonoBehaviour
{
    [SerializeField] private Sound _deathSound;

    private AudioPlayer _audioPlayer;
    private Health _health;

    private void DeathHandler()
    {
        _audioPlayer.Play(_deathSound);
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        _audioPlayer = Context.GetAudioPlayer();
    }

    private void OnEnable()
    {
        _health.OnDied += DeathHandler;
    }

    private void OnDisable()
    {
        _health.OnDied -= DeathHandler;
    }
}