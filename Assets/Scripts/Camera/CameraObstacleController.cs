using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraObstacleController : MonoBehaviour
{
    [SerializeField]private Vector3 _target;
    [SerializeField] private bool _seePlayer;
    [SerializeField] private SummonSoundFromDoor[] _doors;
    [SerializeField] private bool _noDoors=false;
    private Animation _animation;
    [SerializeField] private LayerMask _layerMask;
    private RaycastHit _intHit;
    private Ray _startPosition;

    [SerializeField] private Transform _cameraMovement;


    private void Start()
    {
        _animation = GetComponent<Animation>();
        _animation.Play();
        if (_doors==null)
            _noDoors = true;
        CloseDoors(false);

    }

    private void Update()
    {
        if (!_seePlayer) return;
        _target = GameManager.Instance.PlayerReference.GetHipsPosition();
        if (CheckTarget())
        {
            _animation.Stop();
            _cameraMovement.transform.LookAt(_target);
            GetComponentInChildren<Light>().color = Color.red;
            GetComponentInChildren<Light>().intensity = 6f;
            if (_noDoors) return;
            CloseDoors(true);

        }
        else
        {
            if (_animation.isPlaying) return;
            _animation.Play();
            if (_noDoors) return;
            CloseDoors(false);
           

        }
    }

    public void SetTarget(bool State)
    {
        _seePlayer = State;
    }

    private bool CheckTarget()
    {
         _startPosition = new Ray(transform.position, (_target+ new Vector3(0,0.35f,0) - transform.position));
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
