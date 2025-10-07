using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Crouch : Abstract_Control
{
    public Control_Crouch(PL_Control Controller)
    {
        Controller.AddAction(Execute);
    }
    public override void Execute()
    {
        if (Input.GetKeyDown(_key) || Input.GetKeyDown(_altKey))
        {
            _model.Crouch();
            _view.Crouch();
        }
    }
}
