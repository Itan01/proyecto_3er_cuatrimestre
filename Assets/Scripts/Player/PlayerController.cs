using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public PlayerController()
    {
    }

    public bool CheckMovementInputs(PlayerMovement Script)
    {
        bool isPressingCrouch=false;
        isPressingCrouch = Input.GetKey(KeyCode.C);

        return (Script.CheckIfMoving(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), isPressingCrouch));
    }
    public void CheckGrabGunInput(PlayerGrabbingGun ScriptGrabGun)
    {
        if (Input.GetMouseButton(1))
            ScriptGrabGun.CatchingSound();
        else
            ScriptGrabGun.StopCatching();
    }
    public void CheckShootGunInput(PlayerShootingGun ScriptShootGun)
    {
        bool HasASound = ScriptShootGun.CheckSound();
        if (Input.GetMouseButtonDown(0) && HasASound)
            ScriptShootGun.ThrowSound();
    }
    public void CheckInteractions(PlayerInteractions ScriptInteractions)
    {
        if (Input.GetKeyDown(KeyCode.E))
            ScriptInteractions.Interact();
    }
}
