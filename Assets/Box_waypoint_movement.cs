using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_waypoint_movement : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed=5.0f;
    [SerializeField] private bool _automatic;
    private bool _arrive;
    [SerializeField] private int _index=0;
    private void Start()
    {
        transform.position= _waypoints[0].position;
    }
    private void Update()
    {
        if (_automatic)
        {
            Move();
            return;
        }

        if (!_arrive)
            SlerpMove();

    }

    public void MoveToNextWaypoint()
    {
        _index++;
        if(_index >= _waypoints.Length)
        {
            _index = 0;
        }
            _arrive = false;
    }
    private void SlerpMove()
    {
        float Distance= Vector3.Distance(transform.position, _waypoints[ _index ].position);
        if (Distance < 0.25f)
        {
            transform.position = _waypoints[_index].position;
            _arrive = true;
        }
        else
        {
            transform.position=Vector3.Slerp(transform.position,_waypoints[_index].position,_speed * Time.deltaTime);
        }
    }
    private void Move()
    {
        float Distance = Vector3.Distance(transform.position, _waypoints[_index].position);
        if (Distance < 0.25f)
        {
            transform.position = _waypoints[_index].position;
            MoveToNextWaypoint();
        }
        else
        {
            transform.position += (_waypoints[_index].position - transform.position).normalized * _speed * Time.deltaTime;
        }
    }
}
