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
    [SerializeField] private float _maxDistanceRef,_maxAuxDistance;
    [Range(-90.0f, 0.0f)][SerializeField] private float _minRotation = -85.0f;
    [Range(0.0f, 90.0f)][SerializeField] private float _maxRotation = 75.0f;
    private float _offsetValue = 0.0f;
    [SerializeField] private bool _setCamDis=false, _resetCamDis=false;
    private Vector3 _offSetTarget;

    [Header("LayerMask")]
    [SerializeField] private LayerMask Enviroment;

    private bool _isBlocked = false;
    private float _mouseX = 0.0f, _mouseY = 0.0f, _maxTopDistance= 2.0f;
    private Vector3 _dir = new(), _dirTest = new(), _camPos = new(), _camLookAt= new(), _camDir=new ();

    private Camera _cam;

    private Ray _camRay;
    private RaycastHit _camHit;

    private Coroutine _cameraShakeCoroutine;

    private void Awake()
    {
       GameManager.Instance.CameraReference=transform;
    }
    private void Start()
    {
        _maxDistanceRef = _maxDistance;
        _cam = Camera.main;

        Cursor.lockState = _lockState;
        Cursor.visible = _isCursorVisible;

        transform.forward = _target.forward;

        _mouseX = transform.eulerAngles.y;
        _mouseY = transform.eulerAngles.x;
    }

    private void FixedUpdate()
    {
        _camRay = new Ray(transform.position, _dir);
        _isBlocked = CheckIfEnviroment();   
    }

    private void LateUpdate()
    {
        if (_setCamDis)
            ChangeDistance();
        if (_resetCamDis)
            ResetDistance();
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
        _offSetTarget = -transform.right* _offsetValue;
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
            _camLookAt = _target.transform.position;
        }
        else
        {
            if (_mouseY <= -50.0f)
            {
                _camPos = _target.transform.position + _dir * _maxDistance * ((_maxTopDistance *(-(_mouseY)-50.0f)/100)+1.0f);
                _camDir = _target.transform.position - new Vector3(_camPos.x,_target.transform.position.y, _camPos.z);
                _camLookAt = _target.transform.position + _camDir.normalized * ((_maxTopDistance * (-(_mouseY) - 50.0f) /100)+ 0.1f);
            }
            else
            {
                _camPos = transform.position +  _dir * _maxDistance;
                _camLookAt = _target.transform.position;
            }
        }

        _cam.transform.position = _camPos + _offSetTarget;
        _cam.transform.LookAt(_camLookAt + _offSetTarget);
    }
    private bool CheckIfEnviroment()
    {
        bool aux = Physics.SphereCast(_camRay, _detectionRadius, out _camHit, _maxDistance, Enviroment,QueryTriggerInteraction.Ignore );
        return aux;
    }
    private void ChangeDistance()
    {
        _maxDistance-= Time.deltaTime * 25.0f;
        if (_maxDistance < _maxAuxDistance)
        {
            _maxDistance = _maxAuxDistance;
            _setCamDis = false;
            _offsetValue = 0.5f;
        }
    }
    private void ResetDistance()
    {
        _maxDistance += Time.deltaTime*50.0f;
        if (_maxDistance > _maxAuxDistance)
        {
            _maxDistance = _maxAuxDistance;
            _resetCamDis = false;
            _offsetValue = 0.0f;
        }
    }
    public void SetCameraDistance(float MaxDistance)
    {
        _offsetValue = 0.75f;
        _maxAuxDistance = MaxDistance;
        _setCamDis = true;
    }
    public void ResetCameraDistance()
    {
        _maxAuxDistance = _maxDistanceRef;
        _resetCamDis = true;
    }
    public void TriggerRecoil(float intensity = 0.1f, float duration = 0.15f, float kickbackStrength = 0.15f)
    {
        if (_cameraShakeCoroutine != null)
            StopCoroutine(_cameraShakeCoroutine);
        _cameraShakeCoroutine = StartCoroutine(CameraRecoil(intensity, duration, kickbackStrength));
    }

    private IEnumerator CameraRecoil(float intensity, float duration, float kickbackStrength)
    {
        Vector3 originalPos = _cam.transform.localPosition;
        float elapsed = 0f;

        Vector3 kickbackPos = originalPos - transform.forward * kickbackStrength;

        _cam.transform.localPosition = kickbackPos;

        while (elapsed < duration)
        {
            float recoilX = Random.Range(-intensity, intensity);
            float recoilY = Random.Range(-intensity, intensity);
            float recoilZ = Random.Range(-intensity, intensity);

            _cam.transform.localPosition = kickbackPos + new Vector3(recoilX, recoilY, recoilZ);

            elapsed += Time.deltaTime;
            yield return null;
        }
        float returnDuration = 0.15f;
        elapsed = 0f;
        Vector3 startPos = _cam.transform.localPosition;

        while (elapsed < returnDuration)
        {
            _cam.transform.localPosition = Vector3.Lerp(startPos, originalPos, elapsed / returnDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _cam.transform.localPosition = originalPos;
    }
}

