using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Crouch : Abstract_Control
{
    private Transform _myTransform;
    private LayerMask _mask;
    private bool _isCrouching=false;
    public Control_Crouch(PL_Control Controller)
    {
        Controller.AddAction(Execute);
    }
    public Control_Crouch Transform(Transform MyTransform)
    {
        _myTransform=MyTransform;
        return this;
    }
    public Control_Crouch LayerMask(LayerMask Mask)
    {
        _mask = Mask;
        return this;
    }
    public override void Execute()
    {
        if (Input.GetKeyDown(_key) || Input.GetKeyDown(_altKey))
        {
            if (_isCrouching)
            {
                bool State = Physics.BoxCast(_myTransform.position, new Vector3(0.6f, 0.5f, 0.6f), _myTransform.up, Quaternion.identity, 2.0f, _mask, QueryTriggerInteraction.Ignore);
                _isCrouching = State;
            }    
            else _isCrouching = true;
            _model.Crouch(_isCrouching);
            _view.Crouch(_isCrouching);
        }
    }
    public bool GetState()
    {
        return _isCrouching;
    }
}
