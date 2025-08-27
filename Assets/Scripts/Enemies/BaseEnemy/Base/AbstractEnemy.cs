using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class AbstractEnemy : EntityMonobehaviour, ISoundInteractions
{
    protected NavMeshAgent _agent;
    [SerializeField] protected bool _activate;
    protected QuestionMarkManager _questionMark;
    protected float _baseSpeed = 3.5f, _runSpeed = 7.0F, _shortDistance = 0.5f;
    [SerializeField] protected float _timer = 0.0f, _resetTimer=0.0f;
    [SerializeField] protected int _mode = 0;
    [SerializeField] protected Transform _facingStartPosition;
    protected Vector3 _nextPosition, _startPosition;
    [SerializeField] protected float _confusedDuration;
    protected float _confusedDurationRef = 5.0f; // Este es el tiempo de confusión donde cree haber visto al player
    protected float _searchDuration = 20.0f; // El tiempo que busca al player luego de que este salga del RadiusToHear
    protected float _resetTimerRef = 1.0f;
    [SerializeField] protected bool _watchingPlayer = false;
    protected bool _isRunning = false, _qMBool;
    protected int _qMIndex;
    protected Action Condition, PreMovement, NextMovement;
    protected Action _gamemode;
    [SerializeField] protected EnemyVision _vision;
    protected override void Awake()
    {
        _confusedDuration = _confusedDurationRef;
    }
    protected override void Start()
    {
        _vision = GetComponentInChildren<EnemyVision>();
        RoomManager Room = GetComponentInParent<RoomManager>();
        Room.DetPlayer += CondToTargetPosition;
        Room.FindPlayer += MoveTimerTarget;
        Room.DesActRoom += DesActivation;
        Room.ActRoom += Activation;
        Room.ResRoom += Respawn;
        Room.ResPath += ForceRepath;
        base.Start();

        GetScriptCompo();
        _questionMark = GetComponentInChildren<QuestionMarkManager>();
        if (!Room.IsRoomActivate())
        {
           DesActivation();
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!_activate) return;
        base.Update();
        Condition();
    }
    protected override void FixedUpdate()
    {
    }

    protected virtual void OnCollisionEnter(Collision Entity)
    {
        if (Entity.gameObject.GetComponent<PlayerManager>())
        {
            GameManager.Instance.ResetGameplay();
            SetNewMode(MoveResetPath);
            _nextPosition = _startPosition;
        }
    }

    public void EnterConfusedState()
    {
        SetNewMode(MovConfuse);
    }
    public void Respawn()
    {
       // Debug.Log("Respawn");
        _agent.Warp(_startPosition);
        SetNewMode(MovPatrol);
    }
    #region InternalFunctions
    protected void SetNewMode(Action NewMovement)
    {
        _gamemode = NewMovement;
        _gamemode();
    }
    protected virtual void GetScriptCompo()
    {
    _agent = GetComponent<NavMeshAgent>();
        _startPosition = transform.position;
    }

    protected virtual void TreeNode()
    {
        var ChaseTarget = new EnemyAction(MovChaseTarget);
        var FollowPosition = new EnemyAction(MovFollowPosition);
        var Confused = new EnemyAction(MovConfuse);
        var HearingNoise= new EnemyAction(MoveStartHearing);
        var CheckZone = new EnemyAction(MoveLooking);

        //var CheckWatchingPlayer();
    }

    public void Activation()
    {
        gameObject.SetActive(true);
        _activate = true;
    }
    public void DesActivation()
    {
        gameObject.SetActive(false);
        _activate = false;
    }
    #endregion
    #region TypeOfMovement

    public void SetBehaviourValues(bool isMoving,bool isRunning, bool QMState, int QMSprite, float speed, Action NewCondition, AudioClip clip) 
    { 
        _isMoving = isMoving;
        _isRunning = isRunning;
        _qMBool = QMState;
        _qMIndex = QMSprite;
        _agent.speed = speed;
        Condition = NewCondition;
        if (clip != null)
            _clip = clip;
        ApplyBehaviourValues();
    }
    public void ApplyBehaviourValues()
    {
        _animator.SetBool("isMoving", _isMoving);
        _animator.SetBool("isRunning", _isRunning);
        _questionMark.SetMark(_qMBool, _qMIndex);
        if(_clip !=null)
        _audiosource.PlayOneShot(_clip, 1.0f);
    }
    protected virtual void MoveResetPath(){}
    protected virtual void MovPatrol(){ } // patron normal (Esta en distintos scripts "StandEnemy""StandardEnemy")

    protected virtual void MovChaseTarget() // Persigue al Jugador
    {
        SetBehaviourValues(true,true, true, 1, _baseSpeed, CondChaseTarget, AudioStorage.Instance.StandardEnemySound(EnumAudios.EnemyAlert));
        _mode = 1;
        PreMovement = MovChaseTarget;
        Condition = CondChaseTarget;
        NextMovement = MovChaseTarget;
        _agent.SetDestination(_nextPosition);
    Debug.Log("Mirando Al Jugador");
    }
    protected virtual void MoveTimerTarget() // Persigue al Jugador
    {
        SetNewMode(MovChaseTarget);
    }
    protected void CondChaseTarget()
    {
        _nextPosition = GameManager.Instance.PlayerReference.transform.position;
        _agent.destination = _nextPosition;

    }
    protected void MovFollowPosition() // Camina a una posicion
    {
        SetBehaviourValues(true, false, true, 0, _baseSpeed, CondReachPosition, null);
        _mode = 2;
        PreMovement = MovFollowPosition;
        Condition = CondReachPosition;
        NextMovement = MoveLooking;
        _agent.SetDestination(_nextPosition);
        transform.LookAt(_nextPosition);
        Debug.Log("Yendo a donde escucho");
    }
    protected void CondToTargetPosition() // Persigue al lugar donde se genero el Sonido
    {
        if (_mode == 1) return;
        _nextPosition= GameManager.Instance.PlayerReference.transform.position;
        MovFollowPosition();
        Debug.Log("Yendo a la posible Ubicacion del Player");
    }

    protected void CondReachPosition()
    {
        if (_agent.remainingDistance < _shortDistance)
        {
            SetNewMode(NextMovement);
        }
    }

    protected void MovConfuse()// Confundido (ve al player) es decir, se queda quieto en el lugar pero NO LO BUSCA, esto es solo por vision, no por radiustohear
    {
        SetBehaviourValues(false, false, true, 0, 0.0f, CondTimerConfused, AudioStorage.Instance.StandardEnemySound(EnumAudios.EnemyConfuse));
        _mode = 3;
        _resetTimer = _resetTimerRef;
        _timer = _confusedDuration;
        transform.LookAt(GameManager.Instance.PlayerReference.transform.position);

    }
    protected void CondTimerConfused()
    {
        _timer -= Time.deltaTime;
       
        if (!_watchingPlayer)
        {
            _resetTimer -= Time.deltaTime;
            if (_resetTimer <0)
                SetNewMode(PreMovement);
        }
        if (_timer < 0.0f)
        {
            SetNewMode(MovChaseTarget);
        }
    }
    protected void MoveLooking()
    {
        _mode = 4;
        _timer = 2.5f;
        SetBehaviourValues(false, false, true, 0, 0.0f, CondTimer, AudioStorage.Instance.StandardEnemySound(EnumAudios.EnemyChecking));
        PreMovement = MovPatrol;
        NextMovement = MovPatrol;
        _animator.SetTrigger("isLooking");
        Debug.Log("Viendo alrededor");
    }
    protected void CondTimer()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0.0f)
        {
            SetNewMode(NextMovement);
        }
    }

    protected void MoveStartHearing() // Persigue al Jugador
    {
        _timer = 2.1f;
        _mode = 5;
        SetBehaviourValues(false, false, true, 0, 0.0f, CondTimer, AudioStorage.Instance.StandardEnemySound(EnumAudios.EnemyConfuse));
        _animator.SetTrigger("isHearing");
        PreMovement = MovFollowPosition;
        NextMovement = MovFollowPosition;
    }
    protected void MovStunned() // Persigue al Jugador
    {
        SetBehaviourValues(false, false, false, 0, 0.0f, CondTimer, AudioStorage.Instance.StandardEnemySound(EnumAudios.EnemyHurt));
        _agent.speed = 0.0f;
        _mode = 6;
        _timer = 2.45f;
        _animator.SetTrigger("Stun");
        Condition = CondTimer;
        NextMovement = MovPatrol;
        Debug.Log("Stunned");
    }
    #endregion
    #region Set/Get Values

    public void SetSpeed(float Speed)
    {
        _agent.speed = Speed;
    }
    public void SetModeByIndex(int State)
    {
        if (State == 2)
            SetNewMode(MovFollowPosition);
        else if (State == 3)
            SetNewMode(MovConfuse);
        else if (State == 5)
            SetNewMode(MoveStartHearing);
        else
            SetNewMode(MovPatrol);
    }
    public int GetMode()
    {
        return _mode;
    }
    public void SetPosition(Vector3 pos)
    {
        pos.y = transform.position.y;
        _nextPosition = pos;
    }

    public void WatchingPlayer(bool State)
    {
        _watchingPlayer = State;
    }
    public virtual void SetActivate(bool State)
    {
        _activate = State;
    }
    public bool GetActivate()
    {
        return _activate;
    }
    public void IIteraction(bool PlayerShootIt)
    {
        if (PlayerShootIt)
        {
            Debug.Log("HIII");
            SetNewMode(MovStunned);
        }
    }
    public void ForceRepath()
    {
        if (!_agent.enabled) return;
        _agent.ResetPath();
        _agent.SetDestination(_nextPosition);
    }
    #endregion
    

}
