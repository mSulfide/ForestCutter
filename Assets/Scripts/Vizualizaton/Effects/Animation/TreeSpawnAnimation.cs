using System.Collections;
using System.Linq;
using UnityEngine;

public class TreeSpawnAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve _size;

    private IEnumerator GrownUp()
    {
        if (_size.length <= 0)
            yield break;

        Collider collider = GetComponent<Collider>();
        if (collider != null)
            collider.enabled = false;

        float maxTime = _size.keys.Max(keyframe => keyframe.time);
        float time = _size.keys.Min(keyframe => keyframe.time);

        while (time < maxTime)
        {
            transform.localScale = Vector3.one * _size.Evaluate(time);

            time += Time.deltaTime;
            yield return null;
        }

        if (collider != null)
            collider.enabled = true;
    }

    private void Start()
    {
        StartCoroutine(GrownUp());
    }
}