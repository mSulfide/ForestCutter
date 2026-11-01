using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Update()
    {
        transform.position = _player.MousePosition;
    }
}
