using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerGrabbingGun _scriptGrab;
    private PlayerShootingGun _scriptShoot;
    private PlayerInteractions _scriptInteract;
    private Animator _animator;

    public PlayerController( PlayerGrabbingGun ScriptGrab, PlayerShootingGun ScriptShoot,PlayerInteractions ScriptInteract, Animator Animator)
    {
        _scriptGrab= ScriptGrab;
        _scriptShoot = ScriptShoot;
        _scriptInteract = ScriptInteract;
        _animator = Animator;
    }

    public bool CheckMovementInputs(PlayerMovement Script)
    {
        bool isPressingCrouch=false;
        isPressingCrouch = Input.GetKey(KeyCode.C);

        return (Script.CheckIfMoving(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), isPressingCrouch));
    }
    public void CheckGunInputs()
    {
        if (Input.GetMouseButton(1))
        {
            _scriptGrab.CatchingSound();
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
