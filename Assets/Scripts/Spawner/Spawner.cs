using System;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    protected Vector3 GetClearPosition(Func<Vector3> getPoint, float range = 0f)
    {
        if (getPoint == null)
            throw new ArgumentNullException(nameof(getPoint));
        const int maxIterationCount = 1000;
        for (int i = 0; i < maxIterationCount; i++)
        {
            Vector3 position = getPoint.Invoke();
            if (!IsOccupied(position, range))
                return position;
        }
        return getPoint.Invoke(); // Затычка
        throw new OverflowException();
    }

    private bool IsOccupied(Vector3 position, float range = 0f)
    {
        foreach (var area in transform.GetComponentsInChildren<OccupyingArea>())
            if (area.IsIntersected(position.x, position.z, range))
                return true;
        return false;
    }
}