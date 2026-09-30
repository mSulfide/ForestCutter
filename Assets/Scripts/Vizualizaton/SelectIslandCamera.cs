using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SelectIslandCamera : MonoBehaviour
{
    [SerializeField] private List<Transform> _islands = new();

    private Transform _lastIsland;
    private Camera _camera;
    private Vector3 _position;

    public void GoRight() => GoTo(_islands[Mathf.Clamp(_islands.IndexOf(_lastIsland) + 1, 0, _islands.Count - 1)]);

    public void GoLeft() => GoTo(_islands[Mathf.Clamp(_islands.IndexOf(_lastIsland) - 1, 0, _islands.Count - 1)]);

    private void GoTo(Transform island)
    {
        if (island == null || _lastIsland == island)
            return;

        _position = _camera.transform.position;
        if (_lastIsland != null)
            _position -= _lastIsland.position;

        _lastIsland = island;

        _camera.transform.position = island.position + _position;
    }

    private void Start()
    {
        _camera = GetComponent<Camera>();

        if (_islands.Count > 0)
            _lastIsland = _islands[0];

        GoTo(_islands.Find(islandTransform => islandTransform.TryGetComponent(out SelectIsland island) && island.Level == Context.Game.State.Level));
    }
}