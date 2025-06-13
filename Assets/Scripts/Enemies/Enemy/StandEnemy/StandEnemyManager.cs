using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StandEnemyManager : AbstractEnemy
{
    protected override void Start()
    {
        base.Start();
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
            SetMode(MoveStandPosition);
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
        _nextPosition = _startPosition;
        _agent.destination = _nextPosition;
        //Debug.Log("Origen");
    }
}

