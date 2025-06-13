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
        base.Start();
        for (int i = 0; i < _positions.Length; i++)
        {
            _positions[i] += transform.position;
        }
        _positions[0] = transform.position;
        SetMode(MoveResettingPath);
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
    }
    protected override void NextMovement()
    {
        base.NextMovement();
        if (_mode == 0)
        {
            SetMode(MoveResettingPath);
        }
    }

    protected override void MoveResettingPath() // patron normal (Esta en distintos scripts "StandEnemy""StandardEnemy")
    {
        _mode = 0;
        _isMoving = true;
        _isRunning = false;
        _questionBool = false;
        _questionIndex = 0;
        _agent.speed = _baseSpeed;
        _index++;
        if (_index >= _positions.Length) _index = 0;
        _nextPosition = _positions[_index];
        _agent.destination = _nextPosition;
        _startPosition = _nextPosition;
    }
}
