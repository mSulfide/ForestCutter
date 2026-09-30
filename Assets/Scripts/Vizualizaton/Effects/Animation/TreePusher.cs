using System.Collections;
using System.Linq;
using UnityEngine;

public class TreePusher : MonoBehaviour, IAttackEventListener
{
    [SerializeField] private AnimationCurve _rotationByTime;

    public void AttackHandler(AttackInfo info)
    {
        StartCoroutine(Push(transform.position - info.Position));
    }

    private IEnumerator Push(Vector3 direction)
    {
        direction = new Vector3(direction.x, 0, direction.z);

        Vector3 axis = -Vector3.Cross(direction, Vector3.up).normalized;

        if (_rotationByTime.length <= 0)
            yield break;

        float maxTime = _rotationByTime.keys.Max(keyframe => keyframe.time);
        float time = _rotationByTime.keys.Min(keyframe => keyframe.time);

        while (time < maxTime)
        {
            transform.rotation = Quaternion.AngleAxis(_rotationByTime.Evaluate(time), axis);

            time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.identity;
    }
}