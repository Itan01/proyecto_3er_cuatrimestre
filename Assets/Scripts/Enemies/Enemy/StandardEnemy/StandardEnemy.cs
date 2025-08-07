using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : AbstractEnemy
{
    [SerializeField] protected Vector3[] _positions;
    [SerializeField] protected int _index;
    protected override void Start()
    {
        for (int i = 0; i < _positions.Length; i++)
        {
            _positions[i] += transform.position;
        }
        _positions[0] = transform.position;
        
        base.Start();
        _animator.SetBool("isMoving", true);
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
        _index = 0;
        SetMode(MoveBasePath);
    }
    protected override void MoveBasePath() // patron normal (Esta en distintos scripts "StandEnemy""StandardEnemy")
    {
        _mode = 0;
        _isMoving = true;
        _isRunning = false;
        _questionBool = false;
        _questionIndex = 0;
        _agent.speed = _baseSpeed;
        _nextPosition = _positions[_index];
        _agent.destination = _nextPosition;
        _previousmovement = MoveBasePath;
        _movement = ConditionMoveBasePath;
        _nextmovement = MoveBasePath;

        Debug.Log("Yendo a donde escucho");
    }

    protected void ConditionMoveBasePath()
    {
        if (_agent.remainingDistance <= 0.25f)
        {
            _index++;
            if (_index >= _positions.Length) _index = 0;
            _nextPosition = _positions[_index];
            _agent.destination = _nextPosition;
        }
    }
}
