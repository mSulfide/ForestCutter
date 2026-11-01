using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;
    [SerializeField] private AutoAttacker _autoAttacker;
    [SerializeField] private DropSelectionArea _selection;

    private InputSystem_Actions _actions;
    private Camera _mainCamera;

    public Vector2 MousePosition { get; private set; }

    public Ray Pointer { get; private set; }

    private void PointHandler(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
        Pointer = _mainCamera.ScreenPointToRay(MousePosition);

        _selection.PickUpByRay(Pointer);
    }

    private void AttackHandler(InputAction.CallbackContext context)
    {
        if (context.performed && !_attacker.TryAttackByRay(Pointer))
            _autoAttacker.StartAttack();
        if (context.canceled)
            _autoAttacker.StopAttack();
    }

    private void Awake()
    {
        _actions = new();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _actions.Player.Enable();
        _actions.Player.Attack.performed += AttackHandler;
        _actions.Player.Attack.canceled += AttackHandler;
        _actions.Player.Point.performed += PointHandler;
    }

    private void OnDisable()
    {
        _actions.Player.Disable();
        _actions.Player.Attack.performed -= AttackHandler;
        _actions.Player.Attack.canceled -= AttackHandler;
        _actions.Player.Point.performed -= PointHandler;
    }
}