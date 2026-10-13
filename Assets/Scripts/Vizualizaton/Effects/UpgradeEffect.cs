using UnityEngine;

[RequireComponent(typeof(UpgradeLot))]
public class UpgradeEffect : MonoBehaviour
{
    [SerializeField] private Sound _sound;

    private UpgradeLot _lot;
    private AudioPlayer _audioPlayer;

    private void UpgradeHandler(DealInfo info)
    {
        _audioPlayer.Play(_sound);
    }

    private void Awake()
    {
        _lot = GetComponent<UpgradeLot>();
    }

    private void Start()
    {
        _audioPlayer = Context.GetAudioPlayer();
    }

    private void OnEnable()
    {
        _lot.OnDeal += UpgradeHandler;
    }

    private void OnDisable()
    {
        _lot.OnDeal -= UpgradeHandler;
    }
}