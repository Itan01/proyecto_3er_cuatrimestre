using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movSpeed = 5f, _rotSpeed = 10f ;
    private Rigidbody _rb;
    private Vector3 _dir = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 _viewPlayer;
    [SerializeField] private Transform _orientation, _model, _mainCamera;

    private bool _isMoving = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        _viewPlayer = transform.position - new Vector3(_mainCamera.position.x, transform.position.y, _mainCamera.position.z);
        _orientation.forward = _viewPlayer.normalized;
        _dir.x = Input.GetAxisRaw("Horizontal");
        _dir.z = Input.GetAxisRaw("Vertical");
        _dir = _orientation.forward * _dir.z + _orientation.right * _dir.x;

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
