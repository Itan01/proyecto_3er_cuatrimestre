using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement
{
    private float _movSpeed = 5f, _rotSpeed = 10.0f;
    private Transform _transform, _model, _cam;
    private Rigidbody _rb;
    private Vector3 _dir = new Vector3(0.0f, 0.0f, 0.0f);
    private ControllerAnimator _scriptAnimation;
    private SetSizeCollider _scriptCollider;

    public PlayerMovement(Transform PlayerTransform, Rigidbody PlayerRigibody, Transform CameraTransform, Transform ModelTransform, ControllerAnimator scriptAnimation, SetSizeCollider ScriptCollider)
    {
        _transform = PlayerTransform;
        _rb = PlayerRigibody;
        _cam = CameraTransform;
        _model = ModelTransform;
        _scriptAnimation = scriptAnimation;
        _scriptCollider = ScriptCollider;
    }
    public bool CheckIfMoving(float x, float z, bool crouching)
    {
        _dir = (_cam.forward * z + _cam.right * x).normalized;
        _dir.y = 0.0f;
        SetAnimation(x,z,crouching);
        if (_dir.sqrMagnitude != 0)
        {
            _scriptAnimation.SetBoolAnimator("isMoving", true);
            return true;
        }      
        else
        {
            _scriptAnimation.SetBoolAnimator("isMoving", false);
            return false;
        }
    }
    public void Move()
    {
        _rb.MovePosition(_transform.position + _dir.normalized * _movSpeed * Time.fixedDeltaTime);
        _model.forward = Vector3.Slerp(_model.forward, _dir.normalized, Time.fixedDeltaTime * _rotSpeed);
    }
    private void SetAnimation(float x, float z, bool crouching)
    {
        float y;
        _scriptAnimation.SetBoolAnimator("isCrouching", crouching);
        _scriptAnimation.SetFloatAnimator("xAxis", x);
        _scriptAnimation.SetFloatAnimator("zAxis", z);
        if (crouching)
        {
            _movSpeed = 2.0f;
            y = 1.25f;
        }

        else
        {
            _movSpeed = 5.0f;
            y = 1.75f;
        }
        _scriptCollider.SetSize(y);   
    }

    public void SetMoveZero()
    {
        _dir = Vector3.zero;
    }
}


