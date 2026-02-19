using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Move : Abstract_Model
{
    private float _speed, _refSpeed, _rotSpeed;
    private bool _aiming;
    private bool _isMoving;
    private Vector3 _steer;
    private Transform _dir;
    private Model_Orientation _orientation;
    public Model_Move()
    {
        _rotSpeed = 10.0f;
        _refSpeed = 6.0f;
        _speed = _refSpeed;
        _rb =null;
        _transform=null;
        _dir=Camera.main.transform;
        _orientation = null;
    }
    public Model_Move Speed(float Speed)
    {
        _speed = Speed;
        return this;
    }
    public Model_Move Orientation(Model_Orientation Orientation)
    {
        _orientation = Orientation;
        return this;
    }
    public void ResetSpeed()
    {
        _speed=_refSpeed;
    }
    public void Aiming(params object[] Parameters)
    {
        _aiming = (bool)Parameters[0];
    }
    public override void Execute()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 ViewDir = _transform.position - new Vector3(_dir.position.x,_transform.position.y,_dir.transform.position.z);
        Transform Dir = _transform;
        Dir.forward = ViewDir;
        _steer = (Dir.forward * z + Dir.right * x).normalized;
        if (_steer == Vector3.zero) return;
        Vector3 Orientation = Vector3.Slerp(_modelTransform.forward, _steer, Time.fixedDeltaTime * _rotSpeed);
        _orientation.Set(Orientation);
        _rb.MovePosition(_transform.position + (_speed * Time.fixedDeltaTime * _steer * Save_Progress_Json.Instance.Data.MovementMultiplier));


    }
    public bool IsMoving
    {
        get {return _isMoving; }
        set { _isMoving = value; }
    }
    public void ResetPhysics()
    {
        if (_rb == null) return;

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

}
