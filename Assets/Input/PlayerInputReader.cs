using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public bool CrouchPressed { get; private set; }
    public bool InteractPressed { get; private set; }
    public bool GrabPressed { get; private set; }
    public bool ShootPressed { get; private set; }
    public bool ShootReleased { get; private set; }

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Player.Enable();

        _inputActions.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

        _inputActions.Player.Crouch.performed += _ => ToggleCrouch();
        _inputActions.Player.Interact.performed += _ => InteractPressed = true;
        _inputActions.Player.Grab.performed += _ => GrabPressed = true;
        _inputActions.Player.Grab.canceled += _ => GrabPressed = false;
        _inputActions.Player.Shoot.performed += _ => ShootPressed = true;
        _inputActions.Player.Shoot.canceled += _ =>
        {
            ShootPressed = false;
            ShootReleased = true; // <- Se activa solo un frame
        };
    }

    private void LateUpdate()
    {
        InteractPressed = false; // Reset each frame
        ShootReleased = false; // <- Resetear cada frame
    }

    private void ToggleCrouch()
    {
        CrouchPressed = !CrouchPressed;
    }

    public void ResetShoot()
    {
        ShootPressed = false;
    }

    private void OnDestroy()
    {
        _inputActions.Player.Disable();
    }
}
