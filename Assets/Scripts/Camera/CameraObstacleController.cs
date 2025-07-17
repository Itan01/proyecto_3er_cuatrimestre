using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraObstacleController;

public class CameraObstacleController : MonoBehaviour
{
    private Vector3 _target;
    [SerializeField] private bool _activate=false;
    private bool _rotateToMax = true;
    [SerializeField] private float _rotation, _speedRotation;
    private float _maxRotation, _minRotation, _rotationRef;
    [SerializeField] private bool _seePlayer;
    [SerializeField] private LayerMask _layerMask;
    private RaycastHit _intHit;
    private Ray _startPosition;
    [SerializeField] private Transform _camRotation;
    [SerializeField] private Renderer _cameraLight;
    private RoomManager _room;
    public delegate void Movement();
    private Movement _baseMovement;

    private void Start()
    {
        _maxRotation = _rotation + _camRotation.transform.rotation.y;
        _minRotation = (_rotation * -1f) + _camRotation.transform.rotation.y;
        SetBaseLight();
        GetComponentInParent<RoomManager>().AddToList(this);
        _room =GetComponentInParent<RoomManager>();
    }

    private void Update()
    {
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
            Debug.Log($"Collided obj : {_intHit.collider.name}.");
            if (_intHit.collider.GetComponent<PlayerManager>())
            {
                if (!_activate)
                    SetRedLight();
            }
            else
                SetBaseLight();
        }
        return false;
    }

    private void SetBaseLight()
    {
        _baseMovement = BaseMovement;
        _activate = false;
        _cameraLight.material.SetColor("_Color", Color.yellow);
        _cameraLight.material.SetColor("_EmissionColor", Color.yellow);
        GetComponentInChildren<Light>().color = Color.yellow;
        GetComponentInChildren<Light>().intensity = 3f;
    }
   private void SetRedLight()
    {
        _baseMovement = LookingTarget;
        _room.CameraDetection();
        _activate = true;
        _cameraLight.material.SetColor("_Color", Color.red);
        _cameraLight.material.SetColor("_EmissionColor", Color.red);
        GetComponentInChildren<Light>().color = Color.red;
        GetComponentInChildren<Light>().intensity = 6f;
    }

    private void BaseMovement()
    {
        if (_rotateToMax)
        {

            _rotationRef += _speedRotation * Time.deltaTime;
            _camRotation.Rotate(new Vector3(0.0f, _maxRotation, 0.0f), _speedRotation * Time.deltaTime);
            if (_rotationRef > _maxRotation)
            {
                _rotateToMax = false;
            }

        }
        else
        {
            _rotationRef -= _speedRotation * Time.deltaTime;
            _camRotation.Rotate( new Vector3(0.0f, _minRotation, 0.0f), _speedRotation * Time.deltaTime);
            if (_rotationRef < _minRotation)
            {
                _rotateToMax = true;
            }
        }
    }

    private void LookingTarget()
    {
        _rotationRef = _camRotation.rotation.y;
        _camRotation.LookAt(_target);
        //_camRotation.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(_target - transform.position), 20f * Time.deltaTime);
    }
}
