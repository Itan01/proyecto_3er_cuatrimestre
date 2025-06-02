using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraObstacleController : MonoBehaviour
{
    [SerializeField]private Vector3 _target;
    [SerializeField] private bool _seePlayer;
    [SerializeField] private SummonSoundFromDoor[] _doors;
    private Animation _animation;
    [SerializeField] private LayerMask _layerMask;
    private RaycastHit _intHit;
    private Ray _startPosition;

    private void Start()
    {
        _animation = GetComponent<Animation>();
        _animation.Play();
        CloseDoors(false);
    }

    private void Update()
    {
        if (!_seePlayer) return;
        _target = GameManager.Instance.PlayerReference.GetHipsPosition();
        if (CheckTarget())
        {
            CloseDoors(true);
            _animation.Stop();
            transform.LookAt(_target);
        }
        else
        {      
            if (_animation.isPlaying) return;
            _animation.Play();
            CloseDoors(false);
        }
    }

    public void SetTarget(bool State)
    {
        _seePlayer = State;
    }

    private bool CheckTarget()
    {
         _startPosition = new Ray(transform.position, (_target+ new Vector3(0,0.4f,0) - transform.position));
        if (Physics.Raycast(_startPosition, out _intHit, 20.0f, _layerMask))
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
            Gizmos.DrawRay(transform.position + new Vector3(0, 1.0f, 0), (_target - transform.position) * 20.0f);
    }
    private void CloseDoors(bool State)
    {
        foreach(var Doors in _doors)
        {
            Doors.ForceDoorsClose(State);
        }
    }

}
