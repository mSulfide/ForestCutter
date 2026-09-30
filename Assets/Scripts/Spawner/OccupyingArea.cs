using UnityEngine;

public class OccupyingArea : MonoBehaviour
{
    [SerializeField] private float _range = 1f;

    public float Range => _range;
    private float X => transform.position.x;
    private float Z => transform.position.z;

    public bool IsIntersected(float x, float z, float range = 0f) =>
        (X - x) * (X - x) + (Z - z) * (Z - z) <= (Range + range) * (Range + range);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}