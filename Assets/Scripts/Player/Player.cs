using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;

    private Camera _mainCamera;
    private Vector2 _mousePosition;

    public event Action<Vector2> OnPointMove;
    public event Action<Ray> OnAttackClick;

    public void OnPoint(InputValue value)
    {
        _mousePosition = value.Get<Vector2>();
        OnPointMove?.Invoke(_mousePosition);
    }
    
    public void OnAttack()
    {
        OnAttackClick?.Invoke(_mainCamera.ScreenPointToRay(_mousePosition));
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }
}