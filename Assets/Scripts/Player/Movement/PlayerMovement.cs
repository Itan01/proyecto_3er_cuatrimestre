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
    private PlayerAnimation _scriptAnimation;

    public PlayerMovement(Transform PlayerTransform, Rigidbody PlayerRigibody, Transform CameraTransform, Transform ModelTransform, PlayerAnimation scriptAnimation)
    {
        _transform = PlayerTransform;
        _rb = PlayerRigibody;
        _cam = CameraTransform;
        _model = ModelTransform;
        _scriptAnimation = scriptAnimation;
    }
    public bool CheckIfMoving(float x, float z)
    {
        _dir = (_cam.forward * z + _cam.right * x).normalized;
        _dir.y = 0.0f;
        _scriptAnimation.SetFloatAnimator("xAxis", x);
        _scriptAnimation.SetFloatAnimator("zAxis", z);
        if (_dir.sqrMagnitude != 0)
            return true;
        else
        {

            return false;
        }
    }
    public void Move()
    {
        _rb.MovePosition(_transform.position + _dir.normalized * _movSpeed * Time.fixedDeltaTime);
        _model.forward = Vector3.Slerp(_model.forward, _dir.normalized, Time.fixedDeltaTime * _rotSpeed);
    }
}


