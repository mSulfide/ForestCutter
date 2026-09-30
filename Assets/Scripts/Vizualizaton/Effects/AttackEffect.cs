using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class AttackEffect : MonoBehaviour
{
    [SerializeField] private Sound _attackSound;
    [SerializeField] private Sound _critSound;

    private AudioPlayer _audioPlayer;
    private Attacker _attacker;

    private void AttackHandler(AttackInfo info)
    {
        _audioPlayer.Play(info.IsCrit ? _critSound : _attackSound);
    }

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
    }

    private void Start()
    {
        _audioPlayer = Context.GetAudioPlayer();
    }

    private void OnEnable()
    {
        _attacker.OnAttack += AttackHandler;
    }

    private void OnDisable()
    {
        _attacker.OnAttack -= AttackHandler;
    }
}