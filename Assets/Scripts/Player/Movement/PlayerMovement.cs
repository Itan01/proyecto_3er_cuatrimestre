using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement
{
    public delegate void Movement();
    private Movement _movement;
    private float _movSpeed = 5f,_crouchSpeed=3.5f, _rotSpeed = 10.0f;
    private Transform _transform, _model, _cam;
    private Rigidbody _rb;
    private Vector3 _dir, _orientation;
    private Animator _animator;
    private SetSizeCollider _scriptCollider;
    private bool _isMoving , _isCrouching;

    public PlayerMovement(Transform PlayerTransform, Rigidbody PlayerRigibody, Transform CameraTransform, Transform ModelTransform, Animator Animator, SetSizeCollider ScriptCollider)
    {
        _transform = PlayerTransform;
        _rb = PlayerRigibody;
        _cam = CameraTransform;
        _model = ModelTransform;
        _animator = Animator;
        _scriptCollider = ScriptCollider;
        _movement = BaseMovement;
    }
    public bool CheckIfMoving(float x, float z, bool crouching)
    {
        _isCrouching = crouching;
        _dir = (_cam.forward * z + _cam.right * x).normalized;
        _dir.y = 0.0f;
        SetAnimation(x,z);
        if (_dir.sqrMagnitude != 0)
        {
            _isMoving = true;
            _animator.SetBool("isMoving", _isMoving);
            return true;
        }      
        else
        {
            _isMoving = false;
            _animator.SetBool("isMoving", _isMoving);
            return false;
        }
    }
    public void Move()
    {
        _orientation = _dir;
        _movement();
        if (_animator.GetBool("Grabbing")){
            _orientation = _cam.forward;
            _orientation.y = 0.0f;  
        }
        _model.forward = Vector3.Slerp(_model.forward, _orientation.normalized, Time.fixedDeltaTime * _rotSpeed);
    }
    private void SetAnimation(float x, float z)
    {
        float y;

        _animator.SetBool("isCrouching", _isCrouching);
        _animator.SetFloat("xAxis", x);
        _animator.SetFloat("zAxis", z);
        if (_isCrouching)
        {
            y = 1.0f;
            _movement= CrouchMovement;
        }

        else
        {
            _movement = BaseMovement;
            y = 2.0f;
        }
        _scriptCollider.SetSize(y);   
    }

    public void SetMoveZero()
    {
        _dir = Vector3.zero;
    }
    public bool GetIsMoving()
    {
        return _isMoving;
    }
    public bool GetIsCrouching()
    {
        return _isCrouching;
    }

    private void BaseMovement()
    {
        _rb.MovePosition(_transform.position + _dir.normalized * _movSpeed * Time.fixedDeltaTime);
        //Debug.Log("BaseMovement");
    }
    private void CrouchMovement() 
    {
        _rb.MovePosition(_transform.position + _dir.normalized * _crouchSpeed * Time.fixedDeltaTime);
        //Debug.Log("CrouchMovement");
    }

}


