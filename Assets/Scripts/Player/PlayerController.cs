using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController
{
    private PlayerShootingGun _scriptShoot;
    private PlayerInteractions _scriptInteract;
    private bool _isPressingCrouch = false;
    private bool _canShoot=false, _wantShoot=false;
    private Animator _animator;
    private GrabbingSound _area;

    public PlayerController(PlayerShootingGun ScriptShoot,PlayerInteractions ScriptInteract, Animator Animator, GrabbingSound Area)
    {
        _scriptShoot = ScriptShoot;
        _scriptInteract = ScriptInteract;
        _animator = Animator;
        _area= Area;    
    }

    public bool CheckMovementInputs(PlayerMovement Script)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
            _isPressingCrouch = !_isPressingCrouch;

        return (Script.CheckIfMoving(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), _isPressingCrouch));
    }
    public void CheckGunInputs()
    {
        if (_wantShoot)
        {
            _scriptShoot.Direction();
            if (Input.GetMouseButtonUp(0))
                SetAiming(true);
            else if (Input.GetMouseButtonDown(1))
                SetAiming(false);
        }


        if (Input.GetMouseButton(1))
        {
            _animator.SetBool("Grabbing", true);
            _wantShoot = false;
        }
        else
        {
            _animator.SetBool("Grabbing", false);
            _area.Desactivate();
        }
           


        if (Input.GetMouseButtonDown(0) && _scriptShoot.CheckSound() && _canShoot)
        {
            _scriptShoot.Direction();
            _wantShoot =true;
            UIManager.Instance.AimUI.UITrigger(true);
            GameManager.Instance.CameraReference.GetComponent<CameraManager>().SetCameraDistance(2.0f);
            _animator.SetBool("StartShooting",true);
        }
    }
    public void CheckInteractions()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetTrigger("Steal");
            _scriptInteract.Interact();
        }
           
    }
    public void PlayerCanShootAgain(bool State)
    {
        _canShoot = State;
    }

    private void SetAiming(bool State)
    {
        _wantShoot = false;
        if (State)
        {
            _animator.SetTrigger("Shooting");
            _scriptShoot.ThrowSound();
        }
        _animator.SetBool("StartShooting", false);
        GameManager.Instance.CameraReference.GetComponent<CameraManager>().ResetCameraDistance();
        UIManager.Instance.AimUI.UITrigger(false);
    }
}
