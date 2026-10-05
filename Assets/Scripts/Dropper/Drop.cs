using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Mover))]
public class Drop : MonoBehaviour
{
    private Mover _mover;
    private Action _unsubscribe;

    public bool IsMoving { get; private set; }
    public float Radius => GetComponent<SphereCollider>().radius;

    private void SubscribeToMover()
    {
        if (_mover == null)
            return;
        void onStartMove() => IsMoving = true;
        void onEndMove() => IsMoving = false;
        _mover.OnStartMove += onStartMove;
        _mover.OnEndMove += onEndMove;

        _mover.OnEndMove += TryFallDown;

        _unsubscribe = () =>
        {
            _mover.StopAllCoroutines();
            _mover.OnStartMove -= onStartMove;
            _mover.OnEndMove -= onEndMove;

            _mover.OnEndMove -= TryFallDown;
        };
    }

    private void TryFallDown()
    {
        Vector3 position = Ground.GetPosition(transform.position) ?? transform.position + Vector3.down * 10f;

        const float eps = 0.1f;
        if ((transform.position - position).sqrMagnitude < eps)
            return;

        StartCoroutine(FallDown(position));
    }

    private IEnumerator FallDown(Vector3 position)
    {
        yield return _mover.MoveTo(position, speed: 8f);

        Destroy(gameObject);
    }

    private void UnsubscribeToMover()
    {
        _unsubscribe?.Invoke();
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void Start()
    {
        if (TryGetComponent(out Animator animator))
            animator.Play(0, 0, UnityEngine.Random.Range(0f, 1f));
    }

    private void OnEnable()
    {
        SubscribeToMover();
    }

    private void OnDisable()
    {
        UnsubscribeToMover();
    }
}
