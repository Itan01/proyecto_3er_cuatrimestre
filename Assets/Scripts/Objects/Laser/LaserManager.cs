using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LaserManager : MonoBehaviour , ISoundInteractions
{
    private LineRenderer _lineRenderer;
    private Animation _animation;
    [SerializeField] private Vector3 _offset = new Vector3(0.0f, 0.15f,0.0f);
    private float _maxDistance = 100.0f;
    [SerializeField]private SO_Layers _layer;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private GameObject _explosion;
    private RaycastHit _onHit;
    private Ray _ray;
    private bool _touchPlayer=false;


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
        if (Physics.Raycast(_ray, out _onHit, _maxDistance, _layer._everything, QueryTriggerInteraction.Ignore))
        {
            _lineRenderer.SetPosition(1, _onHit.point);
            if (_onHit.collider.GetComponent<PlayerManager>())
            {
                if (!_touchPlayer)
                {
                    _touchPlayer = true;
                    AudioStorage.Instance.LaserAlarmSound();
                    EventManager.Trigger(EEvents.DetectPlayer);
                }
                //GetComponentInParent<RoomManager>().DetectPlayer();
            }
            else
                _touchPlayer = false;
        }
        else
            _lineRenderer.SetPosition(1, transform.position + _offset + transform.forward * _maxDistance);
        _lineRenderer.SetPosition(0, transform.position + _offset);
        _endPosition.position = _lineRenderer.GetPosition(1);
    }

    public void IIteraction(bool ShootIt)
    {
        if(!ShootIt) return;
        var explosion = Instantiate(_explosion,transform.position,Quaternion.identity);
        Destroy(gameObject, 0.1f);
    }
}
