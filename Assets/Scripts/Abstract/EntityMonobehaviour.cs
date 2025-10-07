using System;
using UnityEngine;

public abstract class EntityMonobehaviour : MonoBehaviour
{
    protected Rigidbody _rb;
    protected Animator _animator;
    protected bool _isMoving = false, _isCrouching=false;
    protected float _speed;
    protected Action VirtualUpdate;
    protected virtual void Awake() { }
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }
    protected virtual void Update()
    {
        if (VirtualUpdate != null)
        VirtualUpdate();
    }
    protected virtual void FixedUpdate()
    {

    }

    public bool IsMoving()
    {
        return _isMoving;
    }
    public bool IsCrouching()
    {
        return _isCrouching;
    }
    public void AddNoiser(INoise Behaviour)
    {
        VirtualUpdate += Behaviour.Noiser;
    }

    public void RemoveNoiser(INoise Behaviour)
    {
        VirtualUpdate -= Behaviour.Noiser;
    }
}
