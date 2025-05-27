using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class StandEnemyManager : AbstractEnemy
{
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
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
        if (_mode == 1) // Escucha al jugador
        {
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isMoving", true);
            _agent.speed = 5.0f;
            _nextPosition = GameManager.Instance.PlayerReference.transform.position;
        }
        else if (_mode == 2)// Escucha un Sonido
        {
            _agent.speed = 3.5f;
            _animator.SetBool("isMoving", true);
            _animator.SetBool("isRunning", false);
        }
        else
        {
            if (_agent.remainingDistance <= 0.2f)
            {
                transform.LookAt(_facingStartPosition.position);
                _animator.SetBool("isMoving", false);
                _animator.SetBool("isRunning", false);
            }

            else
            {
                _agent.speed = 3.5f;
                _animator.SetBool("isMoving", true);
                _animator.SetBool("isRunning", false);
            }
            _nextPosition = _startPosition;
        }
    }
}
