using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemyManager : AbstractEnemy
{
    [SerializeField] private Transform[] _sequencePosition;
    [SerializeField] private int _index=0;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        GameMode();
    }
    protected override void FixedUpdate()
    {
        Move();
    }
    protected override void GameMode()
    {
        base.GameMode();
        if (_mode == 0)
        {
            _agent.speed = 3.5f;
            _animator.SetBool("isMoving", true);
            _animator.SetBool("isRunning", false);
            if (_agent.remainingDistance <= 0.2f)
            {
                _index++;
                if (_index >= _sequencePosition.Length) _index = 0;

                _nextPosition = _sequencePosition[_index].position;
                transform.LookAt(_sequencePosition[_index]);
            }
        }
    }
}
