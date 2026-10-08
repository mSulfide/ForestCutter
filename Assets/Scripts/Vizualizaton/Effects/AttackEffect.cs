using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class AttackEffect : MonoBehaviour
{
    [SerializeField] private Sound _attackSound;
    [SerializeField] private Sound _critSound;
    [SerializeField] private RandomSound _missSound;

    private AudioPlayer _audioPlayer;
    private Attacker _attacker;

    private void AttackHandler(AttackInfo info)
    {
        if (info.Damage > 0)
            _audioPlayer.Play(info.IsCrit ? _critSound : _attackSound);
        else
            _audioPlayer.Play(_missSound);
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
        _attacker.OnAttackAction += AttackHandler;
    }

    private void OnDisable()
    {
        _attacker.OnAttackAction -= AttackHandler;
    }
}