using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class AbsStandardSoundMov : MonoBehaviour // Sonidos Genericos,Movimiento Base
{
    [SerializeField] protected float _speed = 0.0f, _refSpeed = 0.0f, _size = 0.0f, _rotSpeed = 0.0f;
    protected Vector3 _dir = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] protected Transform _target;
    [SerializeField] protected int _limit = 0;
    protected Rigidbody _rb;

    protected virtual void Start(){
        BaseSettings();
    }

    protected virtual void Update()
    {
        if (_target)
            SetDirectionToTarget();
    }
    protected virtual void FixedUpdate(){
            Move();
    }



    protected virtual void Move(){
        _rb.AddForce(_dir * _speed * Time.fixedDeltaTime);
    }

    protected virtual void SetDirectionToTarget()
    {
        _dir = (_target.position- transform.position).normalized;
        transform.forward = Vector3.Slerp(transform.forward, _dir.normalized, Time.fixedDeltaTime );
    }

    public void SetTarget(Transform Target){
        _target = Target;
    }

    public void Spawn(Transform Spawn, Transform Orientation,float Size, float Speed){
        transform.position = Spawn.position;
        _dir = Orientation.position - Spawn.position;
        _size = Size;
        _speed = Speed;
    }

    protected void BaseSettings()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX;
        _rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        if (_size == 0)
            _size = 1.0f;
        if (_refSpeed == 0)
            _refSpeed = 15.0f;
        if (_speed == 0)
            _speed = _refSpeed;
        if (_rotSpeed == 0)
            _rotSpeed = 10.0f;
        if (_limit == 0)
            _limit = 1;
        if (_dir == new Vector3(0.0f,0.0f,0.0f))
            _dir.z = 1.0f;
    }
}

