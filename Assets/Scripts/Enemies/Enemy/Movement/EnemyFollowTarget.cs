using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowTarget
{
    private Vector3 _moveToPosition;
    public bool _hasTarget;
    private Transform _target;
    private Transform _transform;
    private EnemyStandardManager _scriptManager;


    public EnemyFollowTarget(EnemyStandardManager ScriptManager, Transform EnemyPosition)
    {
        _transform = EnemyPosition;
        _scriptManager= ScriptManager;
    }

    public void SetPostionToFollow(Vector3 _position)
    {
        _moveToPosition = new Vector3(_position.x, _transform.position.y, _position.z);
    }

    public void SetTargetToFollow(Transform Target)
    {
        _hasTarget = true;
        _target = Target;
    }

    public Vector3 GetDirection()
    {
        Vector3 _dir, _pointToSee;
        if (_hasTarget)
        {
            _dir = _target.position - _transform.position;
            _pointToSee = _target.position;
        }
        else
        {
            _dir = _moveToPosition - _transform.position;
            _pointToSee = _moveToPosition;
        }
        _transform.LookAt(_pointToSee);
        CheckDistance(_dir);
        return _dir;
    }

    private void CheckDistance(Vector3 dir) 
    {
        if (dir.magnitude >= 15.0f || dir.magnitude <=0.1f)
            Reset();
    }

    public void Reset()
    {
        _scriptManager.SetMode(0);
        _target = null;
        _hasTarget = false;

    }
}
