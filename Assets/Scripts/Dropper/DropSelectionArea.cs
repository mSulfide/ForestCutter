using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSelectionArea : MonoBehaviour
{
    [SerializeField] private float _maxRange = 0f;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private float _pickupSpeed = 10f;
    [SerializeField] private Inventory _inventory;

    private Camera _mainCamera;

    private Inventory Inventory => _inventory ??= GetComponent<Inventory>() ?? gameObject.AddComponent<Inventory>();

    public event Action<Item, uint> OnPickUp;

    private IEnumerator PickUp(Drop drop)
    {
        Item itemType = null;
        uint count = 0;
        if (drop.TryGetComponent(out Inventory inventory))
            foreach (var item in inventory)
            {
                itemType = item;
                count = inventory.CountOf(item);
            }
        if (itemType == null)
            yield break;

        OnPickUp?.Invoke(itemType, count);

        Vector3 endPoint = transform.position;
        if (drop.TryGetComponent(out Mover mover))
            yield return mover.MoveTo(endPoint, speed: _pickupSpeed);
        Inventory.Add(itemType, count);

        Destroy(drop.gameObject);
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private Vector3 t_lastPosition;

    private void Update()
    {
        Plane plane = new(Vector3.up, Vector3.zero);
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 position = plane.Raycast(ray, out float point) ? ray.GetPoint(point) : Vector3.zero;
        float range = Mathf.Clamp((t_lastPosition - position).magnitude / Time.deltaTime / 100f, 0f, _maxRange);
        t_lastPosition = position;

        List<RaycastHit> hits = new(Physics.SphereCastAll(ray, range, _maxDistance));
        hits.Sort((x, y) => x.distance < y.distance ? -1 : 1);
        foreach (var hit in hits)
            if (hit.collider.TryGetComponent(out Drop drop) && !drop.IsMoving)
                StartCoroutine(PickUp(drop));
    }
}