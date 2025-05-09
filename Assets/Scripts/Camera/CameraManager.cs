using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform _target;

    [Header("Cursor")]
    [SerializeField] private CursorLockMode _lockState = CursorLockMode.Locked;
    [SerializeField] private bool _isCursorVisible = false;

    [Header("Physics")]
    [Range(0.05f, 1.0f)][SerializeField] private float _detectionRadius = 0.1f;
    [SerializeField] private float _hitOffset = 0.25f;

    [Header("Settings")]
    [Range(10.0f, 1000.0f)][SerializeField] private float _mouseSensitivity = 500.0f;
    [Range(0.125f, 1.0f)][SerializeField] private float _minDistance = 0.25f;
    [Range(1.0f, 10.0f)][SerializeField] private float _maxDistance = 4.0f;
    [Range(-90.0f, 0.0f)][SerializeField] private float _minRotation = -85.0f;
    [Range(0.0f, 90.0f)][SerializeField] private float _maxRotation = 75.0f;
    [SerializeField] private Vector3 _offSetTarget;

    [Header("LayerMask")]
    [SerializeField] private LayerMask Enviroment;

    private bool _isBlocked = false;
    private float _mouseX = 0.0f, _mouseY = 0.0f, _maxTopDistance= 2.0f;
    private Vector3 _dir = new(), _dirTest = new(), _camPos = new(), _camLookAt= new(), _camDir=new ();

    private Camera _cam;

    private Ray _camRay;
    private RaycastHit _camHit;

    private void Start()
    {
        _cam = Camera.main;

        Cursor.lockState = _lockState;
        Cursor.visible = _isCursorVisible;

        transform.forward = _target.forward;

        _mouseX = transform.eulerAngles.y;
        _mouseY = transform.eulerAngles.x;
        _offSetTarget = new Vector3(0, 0.5f, 0);
    }

    private void FixedUpdate()
    {
        _camRay = new Ray(transform.position, _dir);
        _isBlocked = CheckIfEnviroment();   
    }

    private void LateUpdate()
    {
       UpdateCamRot(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        UpdateSpringArm();
    }

    private void UpdateCamRot(float x, float y)
    {
        transform.position = _target.transform.position;

        if (x == 0.0f && y == 0.0f) return;

        if ( x != 0.0f )
        {
            _mouseX += x * _mouseSensitivity * Time.deltaTime;

            if(_mouseX > 360.0f || _mouseX < -360.0f)
            {
                _mouseX -= 360.0f * Mathf.Sign(_mouseX);
            }
        }

        if ( y != 0.0f )
        {
            _mouseY += y * _mouseSensitivity * Time.deltaTime;

            _mouseY = Mathf.Clamp(_mouseY, _minRotation, _maxRotation);
        }

        transform.rotation = Quaternion.Euler(-_mouseY, _mouseX, 0.0f);
    }

    private void UpdateSpringArm()
    {
        _dir = -transform.forward;

        if (_isBlocked)
        {
            _dirTest = (_camHit.point - transform.position) + (_camHit.normal * _hitOffset);

            if(_dirTest.sqrMagnitude <= _minDistance * _minDistance)
            {
                _camPos = transform.position + _dir * _minDistance;
            }
            else
            {
                _camPos = transform.position + _dirTest;
            }
            _camLookAt = _target.transform.position + _offSetTarget;
        }
        else
        {
            if (_mouseY <= -50.0f)
            {
                _camPos = _target.transform.position + _dir * _maxDistance * ((_maxTopDistance *(-(_mouseY)-50.0f)/100)+1.0f);
                _camDir = _target.transform.position - new Vector3(_camPos.x,_target.transform.position.y, _camPos.z);
                _camLookAt = _target.transform.position + _camDir.normalized * ((_maxTopDistance * (-(_mouseY) - 50.0f) /100)+ 0.1f) + _offSetTarget;
            }
            else
            {
                _camPos = transform.position +  _dir * _maxDistance;
                _camLookAt = _target.transform.position +_offSetTarget;
            }
        }

        _cam.transform.position = _camPos;
        _cam.transform.LookAt(_camLookAt);
    }
    private bool CheckIfEnviroment()
    {
        bool aux = Physics.SphereCast(_camRay, _detectionRadius, out _camHit, _maxDistance, Enviroment);
        return aux;
    }
}
