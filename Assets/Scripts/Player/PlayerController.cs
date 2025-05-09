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
}
