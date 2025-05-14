using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern
{
    private int _index = 0;
    private Transform[] _sequence;
    private Transform _transform;
    private Vector3 _dir= new Vector3();
    private ControlAnimator _scriptAnimator;

    public EnemyPattern(Transform[] Sequence, Transform EnemyPosition, ControlAnimator ControlAnimator)
    {
        _sequence=Sequence;
        _transform=EnemyPosition;
        _scriptAnimator= ControlAnimator;
    }
    private bool DistanceToNextPosition()
    {
        Vector3 NextPosition = _sequence[_index].position;
        Vector3 _distancePosition = _transform.position;
        _distancePosition = NextPosition - _distancePosition;
        _dir = _distancePosition.normalized;
        _transform.LookAt(NextPosition);
        if (_distancePosition.magnitude <= 0.4f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 CheckIfHasArrive()
    {
        bool _hasArrive = DistanceToNextPosition();

        if (_hasArrive)
        {
            _index++;
            if (_index >= _sequence.Length)
            {
                _index = 0;
            }
        }
        return _dir;
    }
}
