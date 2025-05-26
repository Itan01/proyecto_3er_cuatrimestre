using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class StandEnemyManager : EntityMonobehaviour
{
    private EnemyStandardManager _manager;
    private NavMeshAgent _agent;
    [SerializeField] private Transform _facing;
    private Vector3 _startPosition, _nextPosition;
    [SerializeField] private int _mode = 0;
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
    protected override void GetScripts()
    {
        _manager = GetComponent<EnemyStandardManager>();
        _startPosition = transform.position;
        _agent = GetComponent<NavMeshAgent>();  

    }
    private void GameMode()
    {
        if (_mode == 1)
        {
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isMoving", true);
            _nextPosition = GameManager.Instance.PlayerReference.transform.position;
        }
        else if(_mode == 2)
        {
            _animator.SetBool("isMoving", true);
            _animator.SetBool("isRunning", false);
        }
        else
        {
            if (_agent.remainingDistance <= 0.2f)
            {
                transform.LookAt(_facing.position);
                _animator.SetBool("isMoving", false);
                _animator.SetBool("isRunning", false);
            }

            else
            {
                _animator.SetBool("isMoving", true);
                _animator.SetBool("isRunning", false);
            }

            _nextPosition = _startPosition;
        }
    }
    private void Move()
    {
        _agent.SetDestination(_nextPosition);
    }
    public void SetMode(int Mode)
    {
        _mode = Mode;
    }
    private void OnCollisionEnter(Collision Entity)
    {
        if (Entity.gameObject.TryGetComponent<PlayerManager>(out PlayerManager Entityscript))
        {
            Entityscript.SetDeathAnimation();
            _nextPosition = Entity.transform.position;
            SetMode(0);
        }
        if (Entity.gameObject.TryGetComponent<AbstractSound>(out AbstractSound SoundScript))
        {
            _nextPosition= SoundScript.GetStartPoint();
            SetMode(2);
        }
    }
}
