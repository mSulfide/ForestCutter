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
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void OnEnable()
    {
        _health.OnPointsOver += DeathHandler;
    }

    private void OnDisable()
    {
        _health.OnPointsOver -= DeathHandler;
    }
}