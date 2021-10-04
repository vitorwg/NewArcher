using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class InputReader : MonoBehaviour
{
    private PlayerInputActions _inputActions;
    
    // Gameplay
    public event UnityAction<Vector2> OnMoveEvent;
    public event UnityAction<Vector2> OnLookEvent;
    public event UnityAction<bool> OnShootEvent;


    // Menus
    public event UnityAction<bool> MenuPauseEvent;
    public event UnityAction<bool> MenuClickButtonEvent;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Player.Movement.performed += OnMove;
        _inputActions.Player.Look.performed += OnLook;
        _inputActions.Player.Shoot.performed += OnShoot;

        _inputActions.UI.Pause.performed += OnPause;
        _inputActions.UI.Pause.performed += OnClick;
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        bool shootInput = context.ReadValueAsButton();
        OnShootEvent?.Invoke(shootInput);
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        OnLookEvent?.Invoke(lookInput);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        OnMoveEvent?.Invoke(moveInput);
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        bool clickEvent = context.ReadValueAsButton();
        MenuClickButtonEvent?.Invoke(clickEvent);
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        bool pauseInput = context.ReadValueAsButton();
        MenuPauseEvent?.Invoke(pauseInput);
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
        _inputActions.UI.Disable();
    }
}
