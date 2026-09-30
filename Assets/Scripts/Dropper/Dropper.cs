using RandMath;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private float _maxRange = 1.5f;
    [SerializeField] private float _minRange = 1f;
    [SerializeField] private float _speed = 7.5f;
    [SerializeField] private AnimationCurve _traectory;

    public void DropAll()
    {
        foreach (var item in _inventory)
        {
            for (int i = 0; i < _inventory.CountOf(item); i++)
            {
                Drop(item);
            }
            _inventory.Remove(item);
        }
    }

    private void Drop(Item item, uint count = 1)
    {
        Drop drop = Instantiate(item.DropPrefab);

        drop.gameObject.name = item.name;

        Inventory inventory = drop.TryGetComponent(out Inventory currentInventory) ? currentInventory : drop.gameObject.AddComponent<Inventory>();
        inventory.Add(item, count);

        drop.transform.position = transform.position;
        Vector3 position = Rand.GetPosition(_maxRange, _minRange) + transform.position;
        if (drop.TryGetComponent(out Mover mover))
            mover.MoveTo(Ground.GetPosition(position) ?? position, _traectory, _speed);
    }
}
