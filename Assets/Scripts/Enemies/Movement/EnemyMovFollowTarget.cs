using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovFollowTarget : AbsEnemyVariables
{
    private Vector3 _moveToPosition;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (_enable)
        {
            GetDirection();
        }
    }
    protected override void FixedUpdate()
    {
        if (_enable)
        {
            Move();
        }
    }
    public void SetPostionToFollow(Vector3 _position)
    {
        _moveToPosition = _position;
    }

    private void GetDirection()
    {
        float yRef= transform.position.y;   
        transform.LookAt(_moveToPosition);
        _dir = _moveToPosition - transform.position;
        _dir.y = yRef;
        if (_dir.magnitude <= 0.1)
        {
            _enable=false;
        }


    }
}
