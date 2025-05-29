using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraObstacleController : MonoBehaviour
{
    [SerializeField]private Transform _target;
    private Animation _animation;
    [SerializeField] private LayerMask _layerMask;
    private RaycastHit _intHit;
    private Ray _startPosition;

    private void Start()
    {
        _animation = GetComponent<Animation>();
        _animation.Play();
    }

    private void Update()
    {
        if(_target && CheckTarget())
        {
            _animation.Stop();
            transform.LookAt(_target.position);
        }
        else
        {
            if (_animation.isPlaying) return;
            _animation.Play();
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private bool CheckTarget()
    {
         _startPosition = new Ray(transform.position, (_target.position - transform.position)+ new Vector3(0,0.3f,0) *20.0f);
        if (Physics.Raycast(_startPosition, out _intHit, 15.0f, _layerMask))
        {
            Debug.Log($"Collided obj : {_intHit.collider.name}.");

            return (_intHit.collider.TryGetComponent(out PlayerManager PlayerManager));
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(_target != null)
        Gizmos.DrawRay(_startPosition );
    }
}
