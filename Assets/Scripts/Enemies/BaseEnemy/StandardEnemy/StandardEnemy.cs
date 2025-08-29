using System;
using System.Collections;
using UnityEngine;

public class StandardEnemy : AbstractEnemy
{
    [SerializeField] protected Vector3[] _positions;
    [SerializeField] protected int _indexPosition;
    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < _positions.Length; i++)
        {
            _positions[i] += transform.position;
        }
        _positions[0] = transform.position;
        SetNewMode(MovPatrol);
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
    }
    protected override void MoveResetPath() // patron normal (Esta en distintos scripts "StandEnemy""StandardEnemy")
    {
        _indexPosition = 0;
        SetNewMode(MovPatrol);
    }
    protected override void MovPatrol() // patron normal (Esta en distintos scripts "StandEnemy""StandardEnemy")
    {
        _mode = 0;
        _questionMark.SetMark(false, 0);
        _animator.SetBool("isMoving", true);
        _animator.SetBool("isRunning", false);
        _agent.speed = _baseSpeed;
        _nextPosition = _positions[_indexPosition];
        _agent.destination = _nextPosition;
        PreMovement = MovPatrol;
        Condition = CondPatrol;
        NextMovement = MovPatrol;
        Debug.Log("Yendo a donde escucho");
    }

    protected void CondPatrol()
    {
        if (_agent.remainingDistance <= _shortDistance)
        {
            _indexPosition++;
            if (_indexPosition >= _positions.Length) _indexPosition = 0;
            _nextPosition = _positions[_indexPosition];
            _agent.destination = _nextPosition;
        }
    }
}
