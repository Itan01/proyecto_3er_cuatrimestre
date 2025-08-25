using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraObstacleController;

public class CameraObstacleController : MonoBehaviour
{
    private Vector3 _target,_targetHips,_camRotation;
    private bool _detectingPlayer = false, _rotateToMax = true,  _rotationResetted = true;
    [SerializeField] private float _rotation, _speedRotation;
    private float _xRotation, _yRotation, _zRotation;
    private float _maxRotation, _minRotation, _rotationRef;
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _detectorColor;
    private bool _seePlayer;
   private bool _checkingPlayer;
    private LayerMask _layerMask;
    private RaycastHit _intHit, _inHitFeet, _inHitHead;
    private Ray _hipPosition, _feetPosition, _headPosition;
    private Transform _camTransform;
    private bool _activateCam = false;
    [SerializeField] private Renderer _cameraLight;
    private RoomManager _room;
    private AudioSource _audiosrc;
    [SerializeField] private AudioClip _clip;
    Action Movement;

    private void Start()
    {
        _layerMask = LayerManager.Instance.GetLayerMask(EnumLayers.ObstacleWithPlayerMask);
        _audiosrc = GetComponent<AudioSource>();
        _maxRotation = _rotation + _camTransform.transform.rotation.y;
        _minRotation = (_rotation * -1f) + _camTransform.transform.rotation.y;
        _room = GetComponentInParent<RoomManager>();
        _room.DesActRoom += Desactivate;
        _room.ActRoom += Activate;
        if (!_room.IsRoomActivate())
        {
            Desactivate();
        }
    }

    private void Update()
    {
        if (Movement != null)
            Movement();
        if (_seePlayer)
            CheckTarget();
    }

    public void SetTarget(bool State)
    {
        _seePlayer = State;
    }

    #region Behaviours
    private void NormalMovement()
    {
        _camRotation = _camTransform.localEulerAngles;
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
        _camRotation = _camTransform.localEulerAngles;
        ResetRotation();
        if (!_rotationResetted)
        {
            _camTransform.localEulerAngles = new Vector3(_xRotation, _yRotation, _zRotation);
        }
        else
        {
            if(_activateCam)
            SetBase();
        }
           
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
        if (_audiosrc.isPlaying)
            _audiosrc.Stop();
        _clip= AudioStorage.Instance.CameraSound(EnumAudios.CameraResetting);
        _audiosrc.PlayOneShot(_clip);
        Movement = ResettingMovement;
        _detectingPlayer = false;
        _xRotation = _camRotation.x;
        _yRotation = _camRotation.y;
        _zRotation = _camRotation.z;
        SetCameraColor(_baseColor, 1f);
        _room.ResetDetection();
    }
    private void SetBase()
    {
        _rotationRef = 0.0f;
        Movement = NormalMovement;
        _detectingPlayer = false;
        SetCameraColor(_baseColor, 3f);
    }
    private void SetDetector()
    {
        if(_audiosrc.isPlaying)
            _audiosrc.Stop();
        _clip= AudioStorage.Instance.CameraSound(EnumAudios.CameraDetection);
        _audiosrc.PlayOneShot(_clip);
        Movement = LookingMovement;
        _room.DetectPlayer();
        _detectingPlayer = true;
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

    private RaycastHit CheckRayCast(Ray ray, RaycastHit hit)
    {
        if (Physics.Raycast(ray, out hit, 500.0f, _layerMask, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log($"Collided obj : {_intHit.collider.name}.");
            if (hit.collider.GetComponent<PlayerManager>() && !GameManager.Instance.PlayerReference.GetInvisible())
                _checkingPlayer = true;
        }
        return hit;
    }
    private void CheckTarget()
    {
        _checkingPlayer = false;
        _targetHips = GameManager.Instance.PlayerReference.GetHipsPosition();
        _target = GameManager.Instance.PlayerReference.transform.position;
        _hipPosition = new Ray(transform.position, (_targetHips - transform.position) * 500.0f);
        _intHit = CheckRayCast(_hipPosition, _intHit);
        _headPosition = new Ray(transform.position, (_targetHips + new Vector3(0, 0.6f, 0) - transform.position) * 500.0f);
        _inHitHead = CheckRayCast(_headPosition, _inHitHead);
        _feetPosition = new Ray(transform.position, (_target - transform.position) * 500.0f);
        _inHitFeet = CheckRayCast(_feetPosition, _inHitFeet);
        if (_checkingPlayer)
        {
            if (!_detectingPlayer)
                SetDetector();
        }
        else
        {
            if (_detectingPlayer)
                SetReset();
        }
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

    public void Desactivate()
    {
        _activateCam = false;
        Movement = ResettingMovement;
        gameObject.SetActive(false);
    }
    public void Activate()
    {
        _activateCam = true;
        gameObject.SetActive(true);
    }
}
