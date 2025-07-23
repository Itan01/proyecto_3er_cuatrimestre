using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraObstacleController;

public class CameraObstacleController : MonoBehaviour
{
    private Vector3 _target,_targetHips,_camRotation;
    private bool _activate = false, _rotateToMax = true,  _rotationResetted = true;
    [SerializeField] private float _rotation, _speedRotation;
    private float _xRotation, _yRotation, _zRotation;
    private float _maxRotation, _minRotation, _rotationRef;
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _detectorColor;
    private bool _seePlayer;
    [SerializeField] private bool _checkingPlayer;
    [SerializeField] private LayerMask _layerMask;
    private RaycastHit _intHit, _inHitFeet, _inHitHead;
    private Ray _hipPosition, _feetPosition, _headPosition;
    private Transform _camTransform;
    [SerializeField] private Renderer _cameraLight;
    private RoomManager _room;
    private AudioSource _audio;
    public delegate void Movement();
    private Movement _baseMovement;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
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

    private void CheckTarget()
    {
        _checkingPlayer = false;
        _targetHips = GameManager.Instance.PlayerReference.GetHipsPosition();
        _target = GameManager.Instance.PlayerReference.transform.position;
        _hipPosition = new Ray(transform.position, (_targetHips - transform.position) * 500.0f);
        if (Physics.Raycast(_hipPosition, out _intHit, 500.0f, _layerMask, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log($"Collided obj : {_intHit.collider.name}.");
            if (_intHit.collider.GetComponent<PlayerManager>())
                _checkingPlayer = true;

        }
        _headPosition = new Ray(transform.position, (_targetHips + new Vector3(0, 0.6f, 0) - transform.position) * 500.0f);
        if (Physics.Raycast(_headPosition, out _inHitHead, 500.0f, _layerMask, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log($"Collided obj : {_intHit.collider.name}.");
            if (_inHitHead.collider.GetComponent<PlayerManager>())
                _checkingPlayer = true;
        }

        _feetPosition = new Ray(transform.position, (_target - transform.position) * 500.0f);
        if (Physics.Raycast(_feetPosition, out _inHitFeet, 500.0f, _layerMask, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log($"Collided obj : {_intHit.collider.name}.");
            if (_inHitFeet.collider.GetComponent<PlayerManager>())
                _checkingPlayer = true;
        }
        if (_checkingPlayer)
        {
            if (!_activate)
                SetDetector();
        }
        else
        {
            if (_activate)
                SetReset();
        }
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
        if (_audio.isPlaying)
            _audio.Stop();
        AudioStorage.Instance.CameraResetting();
        _baseMovement = ResettingMovement;
        _activate = false;
        _xRotation = _camRotation.x;
        _yRotation = _camRotation.y;
        _zRotation = _camRotation.z;
        SetCameraColor(_baseColor, 1f);
        _room.CameraResetDetection();
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
        if(_audio.isPlaying)
            _audio.Stop();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, _intHit.point);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _inHitHead.point);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, _inHitFeet.point);
    }
}
