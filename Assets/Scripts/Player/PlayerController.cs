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
        if (_wantShoot)
        {
            SetAiming();
        }
        if (Input.GetMouseButton(1))
        {
            _animator.SetBool("Grabbing", true);
            _animator.SetBool("StartShooting", false);
            _wantShoot = false;
        }
        else
            _animator.SetBool("Grabbing", false);


        if (Input.GetMouseButtonDown(0) && _scriptShoot.CheckSound() && _canShoot)
        {
            _wantShoot=true;
            _animator.SetBool("StartShooting",true);
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

    private void SetAiming()
    {
        if(Input.GetMouseButtonUp(0))
        {
            _wantShoot = false;
            _animator.SetTrigger("Shooting");
            _scriptShoot.ThrowSound();
            _animator.SetBool("StartShooting", false);
        }
    }
}
