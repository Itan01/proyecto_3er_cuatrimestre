using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class AbsStandardSoundMov : MonoBehaviour // Sonidos Genericos,Movimiento Base
{
    [SerializeField] protected float _refSpeed = 0.0f,  _originalSize = 1.0f, _speedState = 2.0f;
    protected float _rotSpeed = 0.0f, _plusLimitSize = 0.25f, _subLimitSize = -0.25f;
    public float _speed = 0.0f, _size = 0.0f;
    public int _index = 0;
    [SerializeField] protected Vector3 _dir = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 _startPosition;
    protected Transform _target;
    [SerializeField] protected int _limit = 0;
    [SerializeField] protected bool _statePlusSize = true;
    protected Rigidbody _rb;
    protected ShootingGun _scriptShoot;
    protected GrabbingGun _scriptGrab;

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
    }



    protected virtual void Move()
    {
        _rb.MovePosition(transform.position + _dir.normalized * _speed * Time.fixedDeltaTime);
    }

    protected virtual void SetDirectionToTarget()
    {
        _dir = (_target.position - transform.position).normalized;
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
        if (_rotSpeed == 0)
            _rotSpeed = 10.0f;
        if (_limit == 0)
            _limit = 1;
        if (_plusLimitSize == 0)
            _plusLimitSize = 0.25f;
        if (_subLimitSize == 0)
            _subLimitSize = -0.25f;
    }


    protected void TravelSize()
    {
        if (_size >= 0.25f)
        {
            if (_statePlusSize)
            {
                _size += _plusLimitSize * Time.deltaTime *_speedState;
                if (_size >= _originalSize + _plusLimitSize)
                {
                    _size = _originalSize + _plusLimitSize;
                    _statePlusSize = false;
                }
            }
            else
            {
                _size += _subLimitSize * Time.deltaTime * _speedState;
                if (_size <= _originalSize + _subLimitSize)
                {
                    _size = _originalSize + _subLimitSize;
                    _statePlusSize = true;
                }
            }
            transform.localScale = new Vector3(_size, _size, _size);
        }

    }

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.CompareTag("Player"))
        {
            _scriptGrab = Player.GetComponent<GrabbingGun>();
            _scriptShoot = Player.GetComponent<ShootingGun>();
                _scriptShoot.CheckSound(true);
                _scriptGrab.CheckSound(true);
                _scriptShoot.SetSound(_index, _speed, _size);
                Destroy(gameObject);
        }
    }
}

