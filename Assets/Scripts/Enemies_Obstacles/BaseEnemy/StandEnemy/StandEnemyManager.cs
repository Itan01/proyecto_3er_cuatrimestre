using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StandEnemyManager : AbstractEnemy
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        SetNewMode(MovPatrol);
    }
    protected override void Update()
    {
        base.Update();
        if (_mode == -1)
            transform.LookAt(_nextPosition);
    }
    protected override void FixedUpdate()
    {
    }

    
    protected override void MovPatrol() // patron normal (Esta en distintos scripts "StandEnemy""StandardEnemy")
    {
        _mode = 0;
        _isMoving = true;
        _isRunning = false;
        _qMBool = false;
        _qMIndex = 0;
        _agent.speed = _baseSpeed;
        _nextPosition = _startPosition;
        _agent.destination = _nextPosition;
    }
    protected void MoveStandPosition() // Persigue al Jugador
    {
        _mode = -1;
        _isMoving = false;
        _isRunning = false;
        _qMBool = false;
        _qMIndex = 1;
        _agent.speed = 0.0f;
        _nextPosition = _startPosition;
        _timer = -1f;
        transform.LookAt(_nextPosition);
    }
}

