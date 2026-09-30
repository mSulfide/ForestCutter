using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Death : MonoBehaviour, IAttackEventListener
{
    [SerializeField] private Sound _deathSound;
    [SerializeField] private Dropper _dropper;
    [Header("Animation")]
    [SerializeField] private AnimationCurve _rotation;

    private AudioPlayer _audioPlayer;
    private Health _health;
    private Vector3 _direction = Vector3.zero;

    public void AttackHandler(AttackInfo info)
    {
        _direction = transform.position - info.Position;
    }

    private IEnumerator OnDeath()
    {
        if (TryGetComponent(out Collider collider))
            collider.enabled = false;

        if (_rotation.length > 0)
        {
            float maxTime = _rotation.keys.Max(keyframe => keyframe.time);
            float time = _rotation.keys.Min(keyframe => keyframe.time);

            Vector3 axis = -Vector3.Cross(_direction, Vector3.up).normalized;

            while (time < maxTime)
            {
                transform.rotation = Quaternion.AngleAxis(_rotation.Evaluate(time), axis);

                time += Time.deltaTime;
                yield return null;
            }
        }

        _audioPlayer.Play(_deathSound);

        _dropper.DropAll();

        Destroy(gameObject);
    }

    private void DeathHandler()
    {
        StartCoroutine(OnDeath());
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