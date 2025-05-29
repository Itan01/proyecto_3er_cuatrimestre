using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : AbstractEnemy
{
    [SerializeField] private Vector3[] _positions;
    [SerializeField] private int _index;
    protected override void Start()
    {
        base.Start();
        for(int i =0;i<_positions.Length; i++)
        {
            _positions[i] += transform.position;
        }
        _positions[0]=transform.position;
        _index = 0;
        _nextPosition = _positions[_index];
    }

    // Update is called once per frame
    protected override void Update()
    {
        Timer();
        if(_timer==0.0f)
        Pattern();
    }
    protected override void FixedUpdate()
    {
        Move();
    }
    protected virtual void Pattern()
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
            _animator.SetBool("isMoving", true);
            _animator.SetBool("isRunning", false);
            if (_agent.remainingDistance <= 0.2f)
            {
                _index++;
                _timer = 0.5f;
                if (_index >= _positions.Length)
                {
                    _index = 0;
                }
                transform.LookAt(new Vector3(_positions[_index].x,transform.position.y, _positions[_index].z));
                _nextPosition = _positions[_index];
            }
            else
            {
                _agent.speed = 3.5f;
                _nextPosition = _positions[_index];
            }
        }

    }
}
