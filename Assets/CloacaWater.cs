using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloacaWater : MonoBehaviour
{
    [SerializeField]private Transform _LevelWater;
    [SerializeField] private float _speed=3.0f;
    [SerializeField] private float _timer=0.0f;
    [SerializeField] private bool _dry, _reset;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        _timer = 5.0f;
        _dry = false;
        _reset = false;
    }
    private void Update()
    {
        if (_dry)
        {
            DryWater();
        }
        if (_reset) 
        {
            Reset();
        }
        if (_timer > 0.0f)
        {
            Timer();
        }
    }

    private void DryWater()
    {
        float Distance = Vector3.Distance(transform.position, _LevelWater.position);
        if (Distance < 0.25f)
        {
            transform.position = _LevelWater.position;
        }
        else
        {
            transform.position= Vector3.Slerp( transform.position, _LevelWater.position, _speed * Time.deltaTime);
        }
    }
    public void DryCloaca()
    {
        _timer = 5.0f;
        _dry = true;
        _reset = false;
    }
    private void Reset()
    {
        float Distance = Vector3.Distance(transform.position, _startPosition);
        if (Distance < 0.25f)
        {
            transform.position = _startPosition;
            _dry = false;
            _reset = false;
        }
        else
        {
            transform.position += (_startPosition - transform.position).normalized * _speed * Time.deltaTime;
        }
    }
    private void Timer()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0.0f)
        {
            _reset = true;
            _dry = false;
        }
    }
}
