using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEngine.ParticleSystem;
[RequireComponent(typeof(Rigidbody))]

public class AbstractSound : MonoBehaviour // Sonidos Genericos,Movimiento Base
{
    protected float _maxDistanceRay = 50.0f;
    [SerializeField] protected bool _playerSummoned = false, _playerShooted= false ;
    [SerializeField] protected bool _canCatch = false;
        protected bool _freeze = false;
    protected float _speed = 5.0f, _size = 1.0f;
    protected int _index = 0;
    protected Vector3 _dir = new Vector3(0.0f, 0.0f, 0.0f);
    protected Vector3 _startPosition;
    protected SpriteRenderer _sprite;
    [SerializeField] protected Transform _target;
    protected Rigidbody _rb;
    protected TrailRenderer _trail;


    protected virtual void Start()
    {
        BaseSettings();
        _startPosition = transform.position;
        _trail = GetComponentInChildren<TrailRenderer>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {

        if (!_freeze)
        {
            ReduceSize();
        }

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
        _dir = (_target.position + new Vector3(0, 0.5f, 0) - transform.position).normalized;
        transform.forward = Vector3.Slerp(transform.forward, _dir.normalized, Time.fixedDeltaTime);
    }

    public void SetTarget(Transform Target, float Speed)
    {
        _target = Target;
        if (Speed == 0.0f)
            _speed = 5.0f;
        else
            _speed = Speed;
    }

    public virtual void SetDirection(Vector3 Orientation, float Speed, float Size)
    {
        _dir = Orientation;
        _size = Size;
        if (Speed == 0.0f)
            _speed = 5.0f;
        else
            _speed = Speed;
    }

    protected void BaseSettings()// En Caso de que no se especifique una Variable base
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.freezeRotation = true;
    }


    protected virtual void ReduceSize()
    {
        if (_size >= 0.25f)
        {
            _size -= 0.25f * Time.deltaTime;
            transform.localScale = new Vector3(_size, _size, _size);
            _trail.widthMultiplier = _size;
        }
        else
            Destroy(gameObject);

    }

    public bool GetIfPlayerSummoned()
    {
        return _playerSummoned;
    }

    protected void OnTriggerEnter(Collider Entity)
    {
        if (Entity.TryGetComponent<PlayerManager>(out PlayerManager PlayerScript))
        {
            if (_canCatch)
                PlayerScript.SetSound(_index);
        }
    }
    public bool HasLineOfVision(LayerMask mask, Vector3 EntityPosition)
    {
        RaycastHit hit;
        Vector3 orientation = EntityPosition - transform.position;
        if (Physics.Raycast(transform.position, orientation, out hit, _maxDistanceRay, mask))
        {
            if (hit.collider.gameObject.layer==27)
            {
                _playerSummoned = false;
                return true;   
            }

        }
        return false;
    }
    #region GetValues

    public void SetPlayerShootIt(bool State)
    {
         _playerShooted= State;
    }
    public bool PlayerShootIt()
    {
        return _playerShooted;
    }
    public void PlayerCanCatchIt(bool State)
    {
        _canCatch = State;
    }
    public void SetIfPlayerSummoned(bool State)
    {
        _playerSummoned = State;
    }
    public void FreezeObject(bool State)
    {
        _freeze = State;
    }
    public Vector3 GetStartPoint()
    {
        return _startPosition;
    }
    public int GetIndex()
    {
        return _index;
    }
    #endregion
    public void SetSpawnPoint(Vector3 position)
    {
        _startPosition = position;
    }
}


