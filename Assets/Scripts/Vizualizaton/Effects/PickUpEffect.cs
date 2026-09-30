using UnityEngine;

[RequireComponent(typeof(DropSelectionArea))]
public class PickUpEffect : MonoBehaviour
{
    [SerializeField] private Sound _sound;

    private AudioPlayer _audioPlayer;
    private DropSelectionArea _selection;

    private void PickUpHandler()
    {
        _audioPlayer.Play(_sound);
    }

    private void Awake()
    {
        _selection = GetComponent<DropSelectionArea>();
    }

    private void Start()
    {
        _audioPlayer = Context.GetAudioPlayer();
    }

    private void OnEnable()
    {
        _selection.OnPickUp += PickUpHandler;
    }

    private void OnDisable()
    {
        _selection.OnPickUp -= PickUpHandler;
    }
}