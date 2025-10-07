using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Model_Crouch : Abstract_Model
{
    private Model_Move _move;
    private bool _isCrouching=false;
    private float _crouchSpeed;
    public Model_Crouch(Model_Move Move)
    {
        _crouchSpeed = 3.5f;
        _move = Move;
    }
    public override void Execute()
    {
        if (_isCrouching)
        {
            _isCrouching = false;
            _move.ResetSpeed();
        }
        else
        {
            _isCrouching = true;
            _move.Speed(_crouchSpeed);
        }

    }
}
