using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class AbstractEnemy : EntityMonobehaviour
{
    protected NavMeshAgent _agent;
    [SerializeField] protected int _mode = 0;
    [SerializeField] protected Transform _facingStartPosition;
    [SerializeField] protected Vector3 _nextPosition, _startPosition;
    protected override void Awake()
    {
    }
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
    }
    protected override void FixedUpdate()
    {
    }
    protected override void GetScripts()
    {
        _agent = GetComponent<NavMeshAgent>();
        _startPosition = transform.position;
    }
    protected void Move()
    {
        _agent.SetDestination(_nextPosition);
    }

    public void SetMode(int Mode)
    {
        _mode = Mode;
    }
    protected virtual void GameMode()
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
            if (_agent.remainingDistance <= 0.2f)
                SetMode(0);
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
    protected void OnCollisionEnter(Collision Entity)
    {
        if (Entity.gameObject.TryGetComponent<PlayerManager>(out PlayerManager Entityscript))
        {
            Entityscript.SetDeathAnimation();
            _nextPosition = _startPosition;
            SetMode(0);
        }
        if (Entity.gameObject.TryGetComponent<AbstractSound>(out AbstractSound SoundScript))
        {
            if (SoundScript.GetIfPlayerSummoned())
            {
                _nextPosition = SoundScript.GetStartPoint();
                SetMode(2);

            }
            Destroy(SoundScript.gameObject);
        }
    }
}
