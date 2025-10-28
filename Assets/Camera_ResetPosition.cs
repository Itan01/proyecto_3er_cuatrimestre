using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ResetPosition : Cons_CameraObstacle
{
    private float _speed;
    private float _x,_y,_z;
    private bool _isResetted;
    public Camera_ResetPosition(Fsm_Camera Fsm) : base(Fsm)
    {
        _camera = null;
        _speed = 0.0f;
        _x= 0.0f;   
        _y= 0.0f;
        _z = 0.0f;
    }
    public Camera_ResetPosition Speed(float Speed)
    {
        _speed = Speed;
        return this;
    }
    public override void Enter()
    {
        Debug.Log("Enter To ResetMode");
        _camera.SetColor(_color, 500f);
        _x = _camTransform.localEulerAngles.x;
        _y = _camTransform.localEulerAngles.y;
        _z = _camTransform.localEulerAngles.z;
    }
    public override void Execute()
    {
        if (_camera.SetTarget)
        {
            _fsm.SetNewBehaviour(ECameraBehaviours.watchingEntity);
        }
        ResetRotation();
        if (_isResetted)
        {
            _fsm.SetNewBehaviour(ECameraBehaviours.Base);
            
        }
        _camTransform.localEulerAngles = new Vector3(_x, _y, _z);
    }
    private void ResetRotation()
    {
        _isResetted = true;
        _x = SetValueToZero(_x);
        _y = SetValueToZero(_y);
        _z = SetValueToZero(_z);
    }
    private float SetValueToZero(float number)
    {
        if (number< -359.75f || number < 0.25f)
        {
            number = 0.0f;
            Debug.Log("ValueIsZero");
            return number;

        }
        

        if (number >0 && number <180)
        {
            Debug.Log("addingValue");
            number -= _speed * Time.deltaTime;
            _isResetted = false;
        }
        else if(number > 180 && number < 360)
        {
            Debug.Log("removingValue");
            number += _speed * Time.deltaTime; 
            _isResetted = false;

        }
        return number;
    }
}
