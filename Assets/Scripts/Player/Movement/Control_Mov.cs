using System;
using UnityEngine;

public class Control_Mov : Abstract_Control, IButton
{
    public Control_Mov(PL_Control Controller)
    {
        Controller.AddAction(Execute);
    }
    public override void Execute()
    {
        if (Input.GetAxisRaw("Horizontal") != 0.0f || Input.GetAxisRaw("Vertical") != 0.0f)
        {
            _model.Move();
            _view.Move();
        }
        else
        {
            _view.DontMove();
        }
    }
}


