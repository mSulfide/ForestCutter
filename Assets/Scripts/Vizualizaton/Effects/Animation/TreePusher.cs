using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class TreePusher : MonoBehaviour
{
    [SerializeField] private AnimationCurve _rotationByTime;

    private Health _health;

    private void AttackHandler(AttackInfo info)
    {
        StartCoroutine(Push(transform.position - info.Position, info.Force));
    }

    private IEnumerator Push(Vector3 direction, float force)
    {
        direction = new Vector3(direction.x, 0, direction.z);

        Vector3 axis = -Vector3.Cross(direction, Vector3.up).normalized;

        if (_rotationByTime.length <= 0)
            yield break;

        float maxTime = _rotationByTime.keys.Max(keyframe => keyframe.time);
        float time = _rotationByTime.keys.Min(keyframe => keyframe.time);

        while (time < maxTime)
        {
            transform.rotation = Quaternion.AngleAxis(force * _rotationByTime.Evaluate(time), axis);

            time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.identity;
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
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