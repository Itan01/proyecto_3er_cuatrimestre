using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class AbstractSound : MonoBehaviour // Sonidos Genericos,Movimiento Base
{
    [SerializeField] protected float _refSpeed = 0.0f,  _originalSize = 1.0f, _speedState = 2.0f, _rotSpeed=10.0f;
    private float _maxDistanceRay=20.0f;
    private bool _canCatch = false; 
    public float _speed = 0.0f, _size = 0.0f;
    public int _index = 0;
    [SerializeField] protected Vector3 _dir = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 _startPosition;
    [SerializeField] protected Transform _target;
    protected int _limit = 0;
    protected bool _statePlusSize = true;
    protected Rigidbody _rb;
    protected PlayerShootingGun _scriptShoot;
    protected PlayerGrabbingGun _scriptGrab;

    protected virtual void Start()
    {
        BaseSettings();
        _startPosition = transform.position;
    }

    protected virtual void Update()
    {
        if (_target)
            SetDirectionToTarget();
    }
    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        _rb.MovePosition(transform.position + _dir.normalized * _speed * Time.fixedDeltaTime);
    }

    protected virtual void SetDirectionToTarget()
    {
        _dir = (_target.position + new Vector3(0,1.0f,0) - transform.position).normalized;
        transform.forward = Vector3.Slerp(transform.forward, _dir.normalized, Time.fixedDeltaTime);
    }

    public void SetTarget(Transform Target, float Speed)
    {
        _target = Target;
        if (Speed == 0.0f)
            _speed = _refSpeed;
        else
            _speed = Speed;
    }

    public virtual void SetDirection(Vector3 Orientation, float Speed, float Size)
    {
        _dir = Orientation - transform.position;
        _size = Size;
        _speed = Speed;
    }

    protected void BaseSettings()// En Caso de que no se especifique una Variable base
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX;
        _rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        if (_size == 0)
            _size = _originalSize;
        if (_speed == 0)
            _speed = _refSpeed;
        if (_limit == 0)
            _limit = 1;
        //if (_plusLimitSize == 0)
        //    _plusLimitSize = 0.25f;
        //if (_subLimitSize == 0)
        //    _subLimitSize = -0.25f;
    }


    //protected void TravelSize()
    //{
    //    if (_size >= 0.25f)
    //    {
    //        if (_statePlusSize)
    //        {
    //            _size += _plusLimitSize * Time.deltaTime *_speedState;
    //            if (_size >= _originalSize + _plusLimitSize)
    //            {
    //                _size = _originalSize + _plusLimitSize;
    //                _statePlusSize = false;
    //            }
    //        }
    //        else
    //        {
    //            _size += _subLimitSize * Time.deltaTime * _speedState;
    //            if (_size <= _originalSize + _subLimitSize)
    //            {
    //                _size = _originalSize + _subLimitSize;
    //                _statePlusSize = true;
    //            }
    //        }
    //        transform.localScale = new Vector3(_size, _size, _size);
    //    }

    //}

    private void OnTriggerEnter(Collider Entity)
    {
        if (Entity.TryGetComponent<PlayerManager>(out PlayerManager PlayerScript))
        {
            if (_canCatch)
            {
                PlayerScript.ShootGunSetSound(gameObject);
                Destroy(gameObject);
            }
        }
    }
    public bool HasLineOfVision(LayerMask mask, Vector3 EntityPosition)
    {
        RaycastHit hit;
        Vector3 orientation= EntityPosition - transform.position;
        if (Physics.Raycast(transform.position, orientation, out hit, _maxDistanceRay, mask))
        {

            if (hit.collider.TryGetComponent(out PlayerManager Player))
            {
                return true;
            }

        }
        return false;
    }
    public void PlayerCanCatchIt(bool State)
    {
        _canCatch = State;
    }
}

