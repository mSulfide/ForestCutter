using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class BossTree : MonoBehaviour
{
    private static readonly int GrownDownHash = Animator.StringToHash("GrownDown");

    [SerializeField] private Vector3 _position;
    [SerializeField, Min(0f)] private float _lifeTime = 10f;

    private Health _bossTree;

    private IEnumerator LifeTimeActivate()
    {
        yield return new WaitForSeconds(_lifeTime);

        if (TryGetComponent(out Animator animator))
            animator.Play(GrownDownHash);
    }

    private void GrownDownEndHandler()
    {
        if (TryGetComponent(out Inventory inventory))
            inventory.Clear();

        Destroy(gameObject);
    }

    private void Awake()
    {
        _bossTree = GetComponent<Health>();
    }

    private void Start()
    {
        transform.position = _position;

        if (TryGetComponent(out OccupyingArea area))
        {
            foreach (Health tree in Physics.OverlapSphere(_position, area.Range).Where(collider => collider.TryGetComponent(out Health tree) && tree != _bossTree).Select(collider => collider.GetComponent<Health>()))
            {
                Vector3 point = tree.transform.position + (_position - tree.transform.position).normalized * (tree.TryGetComponent(out CapsuleCollider collider) ? collider.radius : 0);

                AttackInfo info = new()
                {
                    Damage = int.MaxValue,
                    Target = tree,
                    Position = point,
                    Force = 1f
                };

                tree.TakeAttack(info);
            }
        }

        StartCoroutine(LifeTimeActivate());
    }
}