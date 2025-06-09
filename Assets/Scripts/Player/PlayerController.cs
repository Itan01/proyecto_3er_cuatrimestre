using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController
{
    private PlayerShootingGun _scriptShoot;
    private PlayerInteractions _scriptInteract;
    bool _isPressingCrouch = false;
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
        if (Input.GetMouseButton(1))
        {
            _animator.SetBool("Grabbing", true);
        }
        else
            _animator.SetBool("Grabbing", false);


        bool HasASound = _scriptShoot.CheckSound();

        if (Input.GetMouseButtonDown(0) && HasASound)
        {
            _scriptShoot.ThrowSound();
            _animator.SetTrigger("Shooting");
        }
    }
    public void CheckInteractions()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _scriptInteract.Interact();
        }
           
    }
}
