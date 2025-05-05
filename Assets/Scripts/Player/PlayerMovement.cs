using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private string _xAxisName = "xAxis";
    [SerializeField] private string _zAxisName = "zAxis";
    [SerializeField] private float _movSpeed = 5f, _rotSpeed = 10f ;
    private Rigidbody _rb;
    private Animator _animator;
    private Vector3 _dir = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 _viewPlayer;
    [SerializeField] private Transform _model, _mainCamera,_orientation;

    private bool _isMoving = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        _isMoving = CheckIfMoving();

    }
    void FixedUpdate()
    {
        if (_isMoving)
        {
            _rb.MovePosition(transform.position + _dir.normalized * _movSpeed * Time.fixedDeltaTime);
            _model.forward = Vector3.Slerp(_model.forward, _dir.normalized, Time.fixedDeltaTime * _rotSpeed);
        }
    }

    private bool CheckIfMoving()
    {
        _viewPlayer=transform.position -new Vector3(_mainCamera.transform.position.x,transform.position.y, _mainCamera.transform.position.z);
        _orientation.forward= _viewPlayer.normalized;
        _dir.x = Input.GetAxisRaw("Horizontal");
        _dir.z = Input.GetAxisRaw("Vertical");
        _dir = _orientation.forward * _dir.z + _orientation.right * _dir.x;


        _animator.SetFloat(_xAxisName, _dir.x);
        _animator.SetFloat(_zAxisName, _dir.z);
        if (_dir.sqrMagnitude != 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
