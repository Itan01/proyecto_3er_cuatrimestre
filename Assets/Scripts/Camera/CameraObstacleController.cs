using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraObstacleController;

public class CameraObstacleController : MonoBehaviour
{
    private Vector3 _target;
    [SerializeField] private Vector3 _camRotation;
    private bool _activate = false, _rotateToMax = true,  _rotationResetted = true;
    [SerializeField] private float _rotation, _speedRotation;
    private float _xRotation, _yRotation, _zRotation;
    private float _maxRotation, _minRotation, _rotationRef;
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _detectorColor;
    private bool _seePlayer;
    [SerializeField] private LayerMask _layerMask;
    private RaycastHit _intHit;
    private Ray _startPosition;
    private Transform _camTransform;
    [SerializeField] private Renderer _cameraLight;
    private RoomManager _room;
    public delegate void Movement();
    private Movement _baseMovement;

    private void Start()
    {
        _maxRotation = _rotation + _camTransform.transform.rotation.y;
        _minRotation = (_rotation * -1f) + _camTransform.transform.rotation.y;
        SetBase();
        GetComponentInParent<RoomManager>().AddToList(this);
        _room = GetComponentInParent<RoomManager>();
    }

    private void Update()
    {
        _camRotation = _camTransform.localEulerAngles;
        _baseMovement();
        if (_seePlayer)
            CheckTarget();

    }

    public void SetTarget(bool State)
    {
        _seePlayer = State;
    }

    private bool CheckTarget()
    {
        _target = GameManager.Instance.PlayerReference.GetHipsPosition();
        _startPosition = new Ray(transform.position, (_target + new Vector3(0, 0.6f, 0) - transform.position));
        if (Physics.Raycast(_startPosition, out _intHit, 500.0f, _layerMask))
        {
            //Debug.Log($"Collided obj : {_intHit.collider.name}.");
            if (_intHit.collider.GetComponent<PlayerManager>())
            {
                if (!_activate)
                    SetDetector();
            }
            else
                SetReset();
        }
        return false;
    }

    #region Behaviours
    private void BaseMovement()
    {
        if (_rotateToMax)
        {
            _rotationRef += _speedRotation * Time.deltaTime;
            if (_rotationRef > _maxRotation)
            {
                _rotateToMax = false;
            }
        }
        else
        {
            _rotationRef -= _speedRotation * Time.deltaTime;
            if (_rotationRef < _minRotation)
            {
                _rotateToMax = true;
            }
        }
        _camTransform.localEulerAngles = new Vector3(0.0f, _rotationRef, 0.0f);
    }
    private void LookingMovement()
    {
        _camTransform.LookAt(_target);
    }
    private void ResettingMovement()
    {
        ResetRotation();
        if (!_rotationResetted)
        {
            _camTransform.localEulerAngles = new Vector3(_xRotation, _yRotation, _zRotation);
        }

        else
            SetBase();
    }
    private void ResetRotation()
    {
        _rotationResetted = true;
        _xRotation = SetValueToZero(_xRotation);
        _yRotation = SetValueToZero(_yRotation);
        _zRotation = SetValueToZero(_zRotation);
    }
    #endregion

    #region Maths & Setters

    private void SetReset()
    {
        AudioStorage.Instance.CameraResetting();
        _baseMovement = ResettingMovement;
        _activate = false;
        _xRotation = _camRotation.x;
        _yRotation = _camRotation.y;
        _zRotation = _camRotation.z;
        SetCameraColor(_baseColor, 1f);

    }
    private void SetBase()
    {
        _rotationRef = 0.0f;
        _baseMovement = BaseMovement;
        _activate = false;
        SetCameraColor(_baseColor, 3f);
    }
    private void SetDetector()
    {
        AudioStorage.Instance.CameraDectection();
        _baseMovement = LookingMovement;
        _room.CameraDetection();
        _activate = true;
        SetCameraColor(_detectorColor, 6f);

    }

    private float SetValueToZero(float number)
    {
        if (number > 0.25f && number < 180f)
        {
            number -= _speedRotation * Time.deltaTime;
            _rotationResetted = false;
        }
        else if (number < 359.25f && number > 180f)
        {
            number += _speedRotation * Time.deltaTime;
            _rotationResetted = false;
        }
        else
        {
            number = 0.0f;
        }
        return number;
    }
    private void SetCameraColor(Color color, float intensity)
    {
        _cameraLight.material.SetColor("_Color", color);
        _cameraLight.material.SetColor("_EmissionColor", color);
        GetComponentInChildren<Light>().color = color;
        GetComponentInChildren<Light>().intensity = intensity;
    }


    public void SetCamera(Transform cam)
    {
        _camTransform = cam;
    }

    #endregion
}
