using System;
using UnityEngine;

public abstract class EntityMonobehaviour : MonoBehaviour
{
    protected Rigidbody _rb;
    protected Animator _animator;
    protected bool _isMoving = false, _isCrouching=false;
    protected float _speed;
    protected BoxCollider _collider;
    protected Vector3 _posCol,_sizeCol;
    protected Action VirtualUpdate;
    [SerializeField] protected SO_Layers _layer;
    protected virtual void Awake() { }
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _collider= GetComponent<BoxCollider>();
        _sizeCol = _collider.size;
        _posCol = _collider.center;
    }
    protected virtual void Update()
    {
        if (VirtualUpdate != null)
        VirtualUpdate();
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
    public void AddNoiser(INoise Behaviour)
    {
        VirtualUpdate += Behaviour.Noiser;
    }

    public void RemoveNoiser(INoise Behaviour)
    {
        VirtualUpdate -= Behaviour.Noiser;
    }
    public void ResetCollider()
    {
        _collider.size = _sizeCol;
        _collider.center = _posCol;
    }
}
