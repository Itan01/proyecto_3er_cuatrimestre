using System;
using UnityEngine;

public abstract class EntityMonobehaviour : MonoBehaviour
{
    protected Rigidbody _rb;
    protected Animator _animator;
    protected Transform _modelTransform;
    protected bool _isMoving = false, _isCrouching=false;
    protected float _speed;
    protected BoxCollider _collider;
    protected Vector3 _posCol,_sizeCol;
    protected Action VirtualUpdate, VirtualNoise;
    private float _timeNoise=0.5f;
    [SerializeField] protected SO_Layers _layer;
    protected virtual void Awake() { }
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _modelTransform = _animator.transform;
_collider = GetComponent<BoxCollider>();
        _sizeCol = _collider.size;
        _posCol = _collider.center;
    }
    protected virtual void Update()
    {
        VirtualUpdate?.Invoke();
        if(VirtualNoise != null)
        {
            if (!_isCrouching && _isMoving)
                VirtualNoise();
        }
    }
    protected virtual void FixedUpdate()
    {

    }
    public BoxCollider Collider()
    {
        return _collider;
    }
    public bool IsMoving()
    {
        return _isMoving;
    }
    public bool IsCrouching()
    {
        return _isCrouching;
    }
    public SO_Layers Layers()
    {
        return _layer;
    }
    public void AddBehaviour(Action Behaviour)
    {
        VirtualUpdate += Behaviour;
    }

    public void RemoveBehaviour(Action Behaviour)
    {
        VirtualUpdate -= Behaviour;
    }
    public void AddNoise()
    {
        VirtualNoise += Noise;
    }

    public void RemoveNoise()
    {
        VirtualNoise -= Noise;
        _timeNoise = 0.5f;
    }
    public void ResetCollider()
    {
        _collider.size = _sizeCol;
        _collider.center = _posCol;
    }
    private void Noise()
    {
        _timeNoise -= Time.deltaTime;
        if (_timeNoise <= 0)
        {
            _timeNoise = 0.5f;
            var x = Factory_CrashSound.Instance.Create();
            x.transform.position = transform.position+( - _modelTransform.forward + transform.up).normalized *2;
            x.transform.position += _modelTransform.right * UnityEngine.Random.Range(-0.5f, 0.5f);
            x.ForceDirection((x.transform.position - transform.position).normalized);
            x.Speed(2.0f);
               // x.ShootByPlayer = true;
        }
    }
}
