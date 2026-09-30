using System;
using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Coroutine _movement;
    private Coroutine _rotation;

    public event Action OnStartMove;
    public event Action OnEndMove;

    public Coroutine MoveTo(Vector3 position, AnimationCurve traectory = null, float speed = 1f)
    {
        if (_movement != null)
            StopCoroutine(_movement);
        _movement = StartCoroutine(MoveTo(position, traectory ?? AnimationCurve.Linear(0, 0, 1, 0), speed, null));
        return _movement;
    }

    public void Rotate(Vector3 rotationSpeed)
    {
        if (_rotation != null)
            StopCoroutine(_rotation);
        _rotation = StartCoroutine(Rotate(rotationSpeed, null));
    }

    private IEnumerator MoveTo
    (
        Vector3 position,
        AnimationCurve traectory,
        float speed,
        Action callback
    )
    {
        OnStartMove?.Invoke();
        Vector3 startPosition = transform.position;
        float distance = (position - startPosition).magnitude;
        float t = 0f;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(startPosition, position, t);
            if (traectory != null)
                transform.position += traectory.Evaluate(t) * Vector3.up;
            t += Time.deltaTime / distance * speed;
            yield return null;
        }
        transform.position = position;
        OnEndMove?.Invoke();
    }

    private IEnumerator Rotate(Vector3 rotationSpeed, Action callback)
    {
        transform.Rotate(rotationSpeed * UnityEngine.Random.Range(-180f, 180f));
        while (true)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}