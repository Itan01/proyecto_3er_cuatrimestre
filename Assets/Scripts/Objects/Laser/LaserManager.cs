using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserManager : MonoBehaviour , ISoundInteractions
{
    private LineRenderer _lineRenderer;
    private Animation _animation;
    private Vector3 _offset = new Vector3(0.0f,0.475f,0.0f);
    [SerializeField] private float _maxDistance = 10.0f;
    [SerializeField] private LayerMask _ignoreMask;
    [SerializeField] private UnityEvent _onHitTarget;
    [SerializeField] private Transform _endPosition;
    private RaycastHit _onHit;
    private Ray _ray;
    [SerializeField] private GameObject _smokeTrapOn;


    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _animation=GetComponent<Animation>();
        _animation.Play();
    }
    private void Update()
    {
        _ray = new Ray(transform.position + _offset, transform.forward);
        if (Physics.Raycast(_ray, out _onHit, _maxDistance, ~_ignoreMask, QueryTriggerInteraction.Ignore))
        {
            _lineRenderer.SetPosition(1, _onHit.point);
            if (_onHit.collider.GetComponent<PlayerManager>())
            {
                Debug.Log("Hii"); // LLamar al RoomManager
                AudioStorage.Instance.LaserAlarmSound();

                if (_smokeTrapOn.activeSelf == true)
                {
                    return;
                }
                else
                {
                    _smokeTrapOn.SetActive(true);
                    AudioStorage.Instance.LaserAlarmSound();
                }
            }
        }
        else
        {

            _lineRenderer.SetPosition(1, transform.position+ _offset + transform.forward * _maxDistance);
        }
        _lineRenderer.SetPosition(0, transform.position + _offset);
        _endPosition.position = _lineRenderer.GetPosition(1);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawRay(transform.position, transform.forward * _maxDistance);
    //    Gizmos.color= Color.green;
    //    Gizmos.DrawWireSphere(_onHit.point, 0.25f);
    //}
}
