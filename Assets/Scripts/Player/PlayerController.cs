using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController
{
    private PlayerShootingGun _scriptShoot;
    private PlayerInteractions _scriptInteract;
    private bool _canShoot = false, _wantShoot = false;
    private Animator _animator;
    private GrabbingSound _area;
    private PlayerInputReader _input;

    public PlayerController(PlayerShootingGun scriptShoot, PlayerInteractions scriptInteract, Animator animator, GrabbingSound area, PlayerInputReader input)
    {
        _scriptShoot = scriptShoot;
        _scriptInteract = scriptInteract;
        _animator = animator;
        _area = area;
        _input = input;
    }

    public bool CheckMovementInputs(PlayerMovement script)
    {
        return script.CheckIfMoving(_input.MoveInput.x, _input.MoveInput.y, _input.CrouchPressed);
    }

    public void CheckGunInputs()
    {
        if (_wantShoot)
        {
            _scriptShoot.Direction();
            if (!_input.ShootPressed)
                SetGrabing(true);
            else if (_input.GrabPressed == false)
                SetGrabing(false);
        }

        if (_input.GrabPressed)
        {
            _animator.SetBool("Grabbing", true);
            _wantShoot = false;
        }
        else
        {
            _animator.SetBool("Grabbing", false);
            _area.Desactivate();
        }

        // Mostrar apuntado solo si tiene un sonido cargado
        if (_input.ShootPressed && _scriptShoot.CheckSound())
        {
            UIManager.Instance.AimUI.UITrigger(true);
            GameManager.Instance.CameraReference.GetComponent<CameraManager>().SetCameraDistance(2.0f);
            _animator.SetBool("StartShooting", true);
        }
        else
        {
            UIManager.Instance.AimUI.UITrigger(false);
            GameManager.Instance.CameraReference.GetComponent<CameraManager>().ResetCameraDistance();
            _animator.SetBool("StartShooting", false);
        }

        // Lanzar el sonido solo cuando se suelta el botón Y hay un sonido cargado
        if (_input.ShootReleased && _scriptShoot.CheckSound() && _canShoot)
        {
            _scriptShoot.Direction();
            _wantShoot = true;
        }
    }

    public void CheckInteractions()
    {
        if (_input.InteractPressed)
        {
            _animator.SetTrigger("Steal");
            _scriptInteract.Interact();
        }
    }

    public void PlayerCanShootAgain(bool state)
    {
        _canShoot = state;
    }

    private void SetGrabing(bool state)
    {
        _wantShoot = false;
        if (state)
        {
            _animator.SetTrigger("Shooting");
            _scriptShoot.ThrowSound();
        }
        _animator.SetBool("StartShooting", false);
        GameManager.Instance.CameraReference.GetComponent<CameraManager>().ResetCameraDistance();
        UIManager.Instance.AimUI.UITrigger(false);
        _input.ResetShoot();
    }
}
