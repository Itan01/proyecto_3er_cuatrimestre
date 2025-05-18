using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerGrabbingGun _scriptGrab;
    private PlayerShootingGun _scriptShoot;
    private PlayerInteractions _scriptInteract;
    private ControllerAnimator _scriptAnimator;
    public PlayerController( PlayerGrabbingGun ScriptGrab, PlayerShootingGun ScriptShoot,PlayerInteractions ScriptInteract, ControllerAnimator ScriptAnimation)
    {
        _scriptGrab= ScriptGrab;
        _scriptShoot = ScriptShoot;
        _scriptInteract = ScriptInteract;
        _scriptAnimator = ScriptAnimation;
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
            _scriptAnimator.SetBoolAnimator("Grabbing", true);
        }
        else
            _scriptAnimator.SetBoolAnimator("Grabbing", false);


        bool HasASound = _scriptShoot.CheckSound();

        if (Input.GetMouseButtonDown(0) && HasASound)
        {
            _scriptShoot.ThrowSound();
            _scriptAnimator.SetTriggerAnimator("Shooting");
        }
    }
    public void CheckInteractions()
    {
        if (Input.GetKeyDown(KeyCode.E))
            _scriptInteract.Interact();
    }
}
