using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController
{
    private PlayerShootingGun _scriptShoot;
    private PlayerInteractions _scriptInteract;
    private bool _isPressingCrouch = false;
    private bool _canShoot=false, _wantShoot=false;
    private Animator _animator;

    public PlayerController(PlayerShootingGun ScriptShoot,PlayerInteractions ScriptInteract, Animator Animator)
    {
        _scriptShoot = ScriptShoot;
        _scriptInteract = ScriptInteract;
        _animator = Animator;
    }

    public bool CheckMovementInputs(PlayerMovement Script)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
            _isPressingCrouch = !_isPressingCrouch;

        return (Script.CheckIfMoving(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), _isPressingCrouch));
    }
    public void CheckGunInputs()
    {
        if (_wantShoot )
        {
            if(Input.GetMouseButtonUp(0))
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
            _animator.SetBool("Grabbing", false);


        if (Input.GetMouseButtonDown(0) && _scriptShoot.CheckSound() && _canShoot)
        {
            _wantShoot=true;
            _animator.SetBool("StartShooting",true);
            GameManager.Instance.AimUI.UITrigger(true);
            GameManager.Instance.CameraReference.GetComponent<CameraManager>().SetCameraDistance(2.0f);
        }
    }
    public void CheckInteractions()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
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
        else
            _animator.SetBool("StartShooting", false);
        _animator.SetBool("StartShooting", false);
        GameManager.Instance.CameraReference.GetComponent<CameraManager>().ResetCameraDistance();
        GameManager.Instance.AimUI.UITrigger(false);
    }
}
