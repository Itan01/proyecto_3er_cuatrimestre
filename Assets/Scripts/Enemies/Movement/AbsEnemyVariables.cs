using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class AbsEnemyVariables : MonoBehaviour
{
    [SerializeField] protected bool _enable=false;
    [SerializeField] protected float _refSpeed = 0.0f, _rotSpeed = 0.0f;
    protected float _speed = 0.0f;
    protected Rigidbody _rb;
    protected Vector3 _dir;

    protected virtual void Start()
    {
        Setting();
    }

    // Update is called once per frame
    protected virtual void  FixedUpdate()
    {
        
    }
    protected virtual void Update()
    {
    }
    protected virtual void Setting()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = true;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX;
        _rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        if (_refSpeed == 0)
            _refSpeed = 3.5f;
        if (_speed == 0)
            _speed = _refSpeed;
        if (_rotSpeed == 0)
            _rotSpeed = 20.0f;
        if (_dir == new Vector3(0.0f, 0.0f, 0.0f))
            _dir.z = 1.0f;
    }
    protected virtual void Move()
    {
        _rb.MovePosition(transform.position + _dir.normalized * _speed * Time.fixedDeltaTime);
        //_model.forward = Vector3.Slerp(_model.forward, _dir.normalized, Time.fixedDeltaTime * _rotSpeed);
    }
    public virtual void SetActivate(bool mode)
    {
        _enable = mode;
    }
}
