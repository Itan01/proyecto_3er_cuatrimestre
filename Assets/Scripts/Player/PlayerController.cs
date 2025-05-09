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
        return Script.CheckIfMoving(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    public void ChecKGrabGunInput(PlayerGrabbingGun ScriptGrabGun)
    {
        if (Input.GetMouseButtonDown(1))
            ScriptGrabGun.CatchingSound();
    }
    public void ChecKShootGunInput(PlayerShootingGun ScriptShootGun)
    {
        bool HasASound= ScriptShootGun.CheckSound();
        if (Input.GetMouseButtonDown(0) && HasASound)
            ScriptShootGun.ThrowSound();
    }
}
