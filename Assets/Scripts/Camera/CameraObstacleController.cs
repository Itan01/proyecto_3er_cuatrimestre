using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class CameraObstacleController : MonoBehaviour
{
    private Vector3 _target;
    [SerializeField] private bool _seePlayer;
    [SerializeField] private SummonSoundFromDoor[] _doors;
    [SerializeField] private bool _noDoors=false;
    private Animation _animation;
    [SerializeField] private LayerMask _layerMask;
    private RaycastHit _intHit;
    private Ray _startPosition;
    [SerializeField] private AudioClip _cameraSound;
    private float _soundVolume = 0.80f;
    private bool _hasSpottedPlayer = false;
    [SerializeField] private Transform _cameraMovement;
    [SerializeField] private Renderer _cameraLight;

    private void Start()
    {
        _animation = GetComponent<Animation>();
        _animation.Play();
        if (_doors==null)
            _noDoors = true;
        if(_cameraSound==null)
            Debug.Log("No hay sonido");
        CloseDoors(false);

    }

    private void Update()
    {
        if (!_seePlayer) return;
        _target = GameManager.Instance.PlayerReference.GetHipsPosition();
        if (CheckTarget())
        {
            if(!_hasSpottedPlayer)
            {
                if (_cameraSound != null)
                {
                    Debug.Log($"Sonido");
                    AudioManager.Instance.PlaySFX(_cameraSound, _soundVolume);
                }
                _hasSpottedPlayer = true;
            }
            
            _animation.Stop();
            _cameraMovement.transform.LookAt(_target);
            _cameraLight.material.SetColor("_Color", Color.red);
            _cameraLight.material.SetColor("_EmissionColor", Color.red);
            GetComponentInChildren<Light>().color = Color.red;
            GetComponentInChildren<Light>().intensity = 6f;
            if (_noDoors) return;
            CloseDoors(true);

        }
        else
        {
            if (_animation.isPlaying) return;
            _animation.Play();
            _cameraLight.material.SetColor("_Color", Color.yellow);
            _cameraLight.material.SetColor("_EmissionColor", Color.yellow);
            if (_noDoors) return;
            CloseDoors(false);
            _hasSpottedPlayer = false;
        }
    }

    public void SetTarget(bool State)
    {
        _seePlayer = State;
    }

    private bool CheckTarget()
    {
         _startPosition = new Ray(transform.position, (_target+ new Vector3(0,0.6f,0) - transform.position));
        if (Physics.Raycast(_startPosition, out _intHit, 500.0f, _layerMask))
        {
           // Debug.Log($"Collided obj : {_intHit.collider.name}.");

            return (_intHit.collider.GetComponent<PlayerManager>());
           

        }
        return false;
    }
    private void OnDrawGizmos()
    {
        if (CheckTarget())
        {
            Gizmos.color = Color.red;
        }
       else
            Gizmos.color = Color.yellow;
        if (_target != null)
            Gizmos.DrawRay(transform.position, (_target + new Vector3(0, 0.35f, 0) - transform.position));
    }
    private void CloseDoors(bool State)
    {

        foreach (var Doors in _doors)
        {
            Doors.ForceDoorsClose(State);
        }
    }

   
}
