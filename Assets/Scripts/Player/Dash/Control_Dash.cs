using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Control_Dash : Abstract_Control
{
    private float _cd, _maxCD;
    public Control_Dash(PL_Control Controller, float DashCooldown=3.0f)
    {
        Controller.AddAction(Execute);
        _maxCD= DashCooldown;
        _cd= DashCooldown;
    }
    public override void Execute()
    {
        if (!CanDash()) return;
        if (Input.GetKeyDown(_key))
        {
            _cd = _maxCD;
            _model.Dash();
            _view.Dash();
        }

    }
    public bool CanDash()
    {
        _cd-= Time.deltaTime;
        if (_cd <= 0) 
        {
            _cd = 0.0f;
            return true;
        }
        return false;
    }
}
