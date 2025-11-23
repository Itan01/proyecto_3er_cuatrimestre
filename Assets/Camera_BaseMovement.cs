using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_BaseMovement : Cons_CameraObstacle
{
    private float _minRotation,_maxRotation, _speed;
    private bool _rotateToMax = false;
    private float _rotate=0.0f;
    public Camera_BaseMovement(Fsm_Camera Fsm) : base(Fsm)
    {
        _camera = null;
        _minRotation = 0.0f;
        _maxRotation = -0.0f;
        _speed = 0.0f;
    }
    public Camera_BaseMovement Rotation(float Rotation)
    {
        _maxRotation = Rotation + _camTransform.transform.rotation.y;
        _minRotation = (Rotation * -1f) + _camTransform.transform.rotation.y;
        return this;
    }
    public Camera_BaseMovement Speed(float Speed)
    {
        _speed=Speed;
        return this;
    }
    public override void Enter()
    {
        //Debug.Log("Enter To BaseMode");
        _camera.SetColor(_color,9999f);
        _rotate = 0.0f;
    }
    public override void Execute()
    {
        if (_camera.SetTarget)
        { 
            _fsm.SetNewBehaviour(ECameraBehaviours.watchingEntity);
        }
        Vector3 CamRotation = _camTransform.localEulerAngles;
        if (_rotateToMax)
        {
            _rotate += _speed * Time.deltaTime;
            if (_rotate > _maxRotation)
            {
                _rotateToMax = false;
            }
        }
        else
        {
            _rotate -= _speed * Time.deltaTime;
            if (_rotate < _minRotation)
            {
                _rotateToMax = true;
            }
        }
        _camTransform.localEulerAngles = new Vector3(0.0f, _rotate, 0.0f);
    }
    public override void FixedExecute()
    {

    }

}
