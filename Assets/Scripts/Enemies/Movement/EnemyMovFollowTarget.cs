using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovFollowTarget : AbsEnemyVariables
{
    [SerializeField] private Vector3 _moveToPosition;
    private EnemyController _scriptController;
    public bool _hasTarget;
    private Transform _target;
    private Vector3 _distanceTarget;

    protected override void Start()
    {
        base.Start();
        _scriptController = GetComponent<EnemyController>();
    }

    protected override void Update()
    {
        if (!_enable) return;
        GetDirection();
    }

    protected override void FixedUpdate()
    {
        if (!_enable) return;
        Move();
    }

    public override void SetActivate(bool mode)
    {
        base.SetActivate(mode);

        if (!mode)
        {
            if (_rb != null)
            {
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
        }
    }

    public void SetPostionToFollow(Vector3 _position)
    {
        _moveToPosition = new Vector3(_position.x, transform.position.y, _position.z);
    }

    public void SetTargetToFollow(Transform Target)
    {
        _hasTarget = true;
        _target = Target;
    }

    private void GetDirection()
    {
        float yRef = transform.position.y;
        transform.LookAt(_moveToPosition);

        if (_hasTarget)
        {
            _dir = _target.position - transform.position;
            transform.LookAt(_target);
            CheckDistance();
        }
        else
        {
            _dir = _moveToPosition - transform.position;
        }

        _dir.y = yRef;
        if (_dir.magnitude <= 0.1f)
        {
            Reset();
        }
    }

    private void CheckDistance()
    {
        _distanceTarget = _target.position - transform.position;
        if (_distanceTarget.magnitude >= 15.0f)
        {
            Reset();
        }
    }

    public void Reset()
    {
        _enable = false;
        _target = null;
        _hasTarget = false;
        _scriptController.SetTypeOfMovement(1);
    }
}
