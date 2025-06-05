using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;
public abstract class AbstractEnemy : EntityMonobehaviour
{
    protected NavMeshAgent _agent;
    [SerializeField] protected QuestionMarkManager _questionMark;
    protected float _timer = 0.0f;
    protected float _baseSpeed = 3.5f, _runSpeed = 7.5f;
    [SerializeField] protected int _mode = 0;
    [SerializeField] protected Transform _facingStartPosition;
    [SerializeField] protected Vector3 _nextPosition, _startPosition;
    protected override void Awake()
    {
    }
    protected override void Start()
    {
        base.Start();
        GameManager.Instance.RegisterEnemy(this);
        _questionMark = GetComponentInChildren<QuestionMarkManager>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        Timer();
        GameMode();

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
        _agent.destination = _nextPosition;
        if (_agent.remainingDistance <= 0.25f && _timer == 0.0f)
        {
            SetMode(0);
        }
    }

    public void SetMode(int Mode)
    {
        _mode = Mode;
    }
    protected void Timer()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0.0f)
        {
            _timer = 0.0f;
        }
    }


    protected virtual void GameMode()
    {
        if (_mode == 1) // Escucha al jugador
        {
            _questionMark.Setting(true, 1);
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isMoving", true);
            _agent.speed = _runSpeed;
            _nextPosition = GameManager.Instance.PlayerReference.transform.position;
            transform.LookAt(_nextPosition);
        }
        else if (_mode == 2) // Escucha un Sonido
        {
            _questionMark.Setting(true, 0);
            _agent.speed = _baseSpeed;
            _animator.SetBool("isMoving", true);
            _animator.SetBool("isRunning", false);
        }
        else
        {
            _questionMark.Setting(false, 0);
            transform.LookAt(_nextPosition);
            if ((_startPosition - transform.position).magnitude < 0.25f)
            {
                transform.LookAt(_facingStartPosition.position);
                _animator.SetBool("isMoving", false);
                _animator.SetBool("isRunning", false);
            }
            else
            {
                _agent.speed = _runSpeed / 2;
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
            if (!Entityscript.IsPlayerDeath())
            {
                Entityscript.SetDeathAnimation();
                SetMode(0);
                _nextPosition = _startPosition;
                _timer = 1.0f;

            }


        }
        if (Entity.gameObject.TryGetComponent<AbstractSound>(out AbstractSound SoundScript))
        {
            if (SoundScript.GetIfPlayerSummoned() && _mode !=1)
            {
                _nextPosition = SoundScript.GetStartPoint();
                SetMode(2);
                _timer = 1.0f;
            }
            Destroy(SoundScript.gameObject);
        }
    }
    public int GetMode()
    {
        return _mode;
    }
    public void SetPosition(Vector3 pos)
    {
        _nextPosition = pos;
    }
    public void Respawn()
    {
        transform.position = _startPosition;
        _nextPosition = _startPosition;
        SetMode(0);
        _agent.ResetPath();
        _animator.SetBool("isMoving", false);
        _animator.SetBool("isRunning", false);
    }

}
