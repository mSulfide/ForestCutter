using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeCascade : MonoBehaviour
{
    public delegate void HitAction(CascadeInfo info);

    private CapsuleCollider _treeCollider;
    private Health _tree;
    private Vector3 _attackDirection;
    private CascadeInfo _cascade;

    private void AttackHandler(AttackInfo info)
    {
        Vector3 direction = transform.position - info.Position;
        direction.y = 0;
        _attackDirection = direction.normalized;

        _cascade = info.Cascade;
    }

    private void DeathHandler()
    {
        if (_cascade.Count > 0)
            StartCoroutine(WaitCascade());
    }

    private IEnumerator WaitCascade()
    {
        float radius = _treeCollider.radius;
        Vector3 castCenter = transform.position + transform.up * radius;
        List<RaycastHit> hits = new(Physics.SphereCastAll(new(castCenter, _attackDirection), radius, _treeCollider.height).Where(hit => hit.collider.TryGetComponent(out Health health) && health.IsAlive));
        foreach (RaycastHit hit in hits)
        {
            yield return new WaitUntil(() => _treeCollider.bounds.Intersects(hit.collider.bounds));

            Health aim = hit.collider.GetComponent<Health>();

            if (!aim.IsAlive)
                continue;

            CascadeInfo cascade = _cascade;
            cascade.Count -= 1;

            AttackInfo info = new()
            {
                Damage = Mathf.RoundToInt(_tree.Max * _cascade.Force),
                Target = aim,
                Position = hit.point,
                Force = 1f,
                ArmorIgnore = 1f,
                Cascade = cascade
            };

            aim.TakeAttack(info);
        }
    }

    private void Awake()
    {
        if (TryGetComponent(out CapsuleCollider collider) && TryGetComponent(out Health tree))
        {
            _treeCollider = collider;
            _tree = tree;
        }
        else
        {
            throw new MissingComponentException();
        }
    }

    private void OnEnable()
    {
        _tree.OnAttacked += AttackHandler;
        _tree.OnPointsOver += DeathHandler;
    }

    private void OnDisable()
    {
        _tree.OnAttacked -= AttackHandler;
        _tree.OnPointsOver -= DeathHandler;
    }
}