using UnityEngine;

public class Ground : MonoBehaviour
{
    public static Vector3? GetPosition(Vector3 position)
    {
        float x = position.x;
        float z = position.z;
        foreach (RaycastHit hit in Physics.RaycastAll(new Ray(new Vector3(x, 10f, z), Vector3.down), 20f))
            if (hit.transform.GetComponent<Ground>() != null)
                return hit.point;
        return null;
    }
}