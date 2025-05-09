using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public PlayerController()
    {

    }

    public void CheckMovementInputs(PlayerMovement Script)
    {
        Script.CheckIfMoving(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    public void ChecKMouseInputs(PlayerGrabbingGun ScriptGrab, PlayerShootingGun ScriptShoot)
    {
        if (Input.GetMouseButtonDown(1))
        {
            ScriptGrab.CatchingSound();
        }
    }
}
