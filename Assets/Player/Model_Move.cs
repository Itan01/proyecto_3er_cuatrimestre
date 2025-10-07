using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Move : Abstract_Model
{
    private float _speed, _refSpeed, _rotSpeed;
    private Vector3 _orientation;
    private Transform _dir;
    public Model_Move()
    {
        _rotSpeed = 10.0f;
        _refSpeed = 6.0f;
        _speed = _refSpeed;
        _rb =null;
        _transform=null;
        _dir=Camera.main.transform;
    }
    public Model_Move Speed(float Speed)
    {
        _speed = Speed;
        return this;
    }
    public void ResetSpeed()
    {
        _speed=_refSpeed;
    }
    public override void Execute()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _orientation = (_dir.forward * z+ _dir.right * x).normalized;
        _orientation.y = 0.0f;
        _rb.MovePosition(_transform.position + (_speed * Time.fixedDeltaTime * _orientation));
        _modelTransform.forward= Vector3.Slerp(_modelTransform.forward, _orientation, Time.fixedDeltaTime * _rotSpeed);
    }
}
