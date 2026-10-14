using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DropSelectionArea))]
public class StatPickUpListener : MonoBehaviour
{
    [SerializeField, Min(.01f)] private float _delay = 1f;

    private readonly Dictionary<Item, uint> _buffer = new();
    private DropSelectionArea _area;
    private float _time = 0f;

    private IEnumerator PostponedAddRecord()
    {
        _time = _delay;
        while (_time > 0f)
        {
            _time -= Time.deltaTime;
            yield return null;
        }
        AddRecord();
    }

    private void PickUpHandler(Item item, uint count)
    {
        if (item == null || count == 0)
            return;

        if (_buffer.ContainsKey(item))
            _buffer[item] += count;
        else
            _buffer.Add(item, count);

        if (_time > 0f)
            _time = _delay;
        else
            StartCoroutine(PostponedAddRecord());
    }

    private void AddRecord()
    {
        foreach (var item in _buffer)
        {
            StatInventoryChangeInfo info = new()
            {
                Item = item.Key.name,
                Count = (int)item.Value
            };

            Context.Stats.AddRecord(info);
        }
        _buffer.Clear();
    }

    private void Awake()
    {
        _area = GetComponent<DropSelectionArea>();
    }

    private void OnEnable()
    {
        _area.OnPickUp += PickUpHandler;
    }

    private void OnDisable()
    {
        _area.OnPickUp -= PickUpHandler;
    }
}