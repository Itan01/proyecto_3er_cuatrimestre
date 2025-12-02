using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class AbstractEnemy : EntityMonobehaviour, ISoundInteractions
{
    protected NavMeshAgent _agent;
    [SerializeField] protected bool _activate;
    [SerializeField] protected QuestionMarkManager _questionMark;
    protected float _baseSpeed = 3.5f, _runSpeed = 10.0f, _shortDistance = 0.5f;
    [SerializeField] protected float _timer = 0.0f, _resetTimer = 0.0f;
    [SerializeField] protected int _mode = 0;
    protected Vector3 _nextPosition,_startPosition;
    [SerializeField] protected float _confusedDuration;
    protected float _confusedDurationRef = 5.0f; // Este es el tiempo de confusión donde cree haber visto al player
    protected float _searchDuration = 20.0f; // El tiempo que busca al player luego de que este salga del RadiusToHear
    protected float _resetTimerRef = 1.0f;
    [SerializeField] protected bool _watchingPlayer = false;
    protected bool _isRunning = false, _qMBool;
    protected int _qMIndex;
    protected Action Condition = null, PreMovement = null, NextMovement = null;
    protected Action _gamemode = null;
    protected EnemyVision _vision;
    protected RoomManager _room;
    protected AudioClip _clip;
    protected AudioSource _audiosource;
    protected override void Awake()
    {
        _confusedDuration = _confusedDurationRef;
    }
    protected override void Start()
    {
        _room = GetComponentInParent<RoomManager>();
        _room.DestroyRoom += Destroy;
        _room.DesActRoom += DesActivation;
        _room.ActRoom += Activation;
        base.Start();
        GetScriptCompo();
        _vision = GetComponentInChildren<EnemyVision>();
        _audiosource = GetComponent<AudioSource>();
        EventManager.Subscribe(EEvents.DetectPlayer,SetPlayerPosition);
        EventManager.Subscribe(EEvents.ReStart,SetBaseBehaviour);
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
            EventManager.Trigger(EEvents.Reset);
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
        if (NewMovement == null) return;
        _gamemode = NewMovement;

        _gamemode();
    }
    protected void SetPlayerPosition( params object[] parameters)
    {
        if (!_activate) return;
        _nextPosition = GameManager.Instance.PlayerReference.transform.position;
        _gamemode = MovFollowPosition;

        _gamemode();
    }
    protected void SetBaseBehaviour(params object[] parameters)
    {
        MoveResetPath();
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
        var HearingNoise = new EnemyAction(MoveStartHearing);
        var CheckZone = new EnemyAction(MoveLooking);

        //var CheckWatchingPlayer();
    }

    public void Activation()
    {
        _vision.gameObject.SetActive(true);
        _activate = true;
    }
    public void DesActivation()
    {
        _vision.gameObject.SetActive(false);
        _activate = false;
    }
    public void Destroy()
    {
        RoomManager Room = GetComponentInParent<RoomManager>();
        Room.DestroyRoom -= Destroy;
        Room.DesActRoom -= DesActivation;
        Room.ActRoom -= Activation;
        _activate = false;
        gameObject.SetActive(false);
    }
    #endregion
    #region TypeOfMovement

    protected void SetBehaviourValues(bool isMoving, bool isRunning, bool QMState, int QMSprite, float speed, Action NewCondition)
    {
        _isMoving = isMoving;
        _isRunning = isRunning;
        _qMBool = QMState;
        _qMIndex = QMSprite;
        _agent.speed = speed;
        Condition = NewCondition;

        ApplyBehaviourValues();
    }
    public void ApplyBehaviourValues()
    {
        _animator.SetBool("isMoving", _isMoving);
        _animator.SetBool("isRunning", _isRunning);
        _questionMark.SetMark(_qMBool, _qMIndex);
    }
    protected virtual void MoveResetPath() { }
    protected virtual void MovPatrol() { } // patron normal (Esta en distintos scripts "StandEnemy""StandardEnemy")

    protected virtual void MovChaseTarget() // Persigue al Jugador
    {
        SetBehaviourValues(true, true, true, 1, _runSpeed, CondChaseTarget);
        _clip = AudioStorage.Instance.StandardEnemySound(EAudios.EnemyAlert);
        _audiosource.PlayOneShot(_clip, 1.0f);
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
        SetBehaviourValues(true, false, true, 0, _baseSpeed, CondReachPosition);
        _mode = 2;
        PreMovement = MovFollowPosition;
        NextMovement = MoveLooking;
        _agent.SetDestination(_nextPosition);
        transform.LookAt(_nextPosition);
        Debug.Log("Yendo a donde escucho");
    }
    protected void CondToTargetPosition() // Persigue al lugar donde se genero el Sonido
    {
        if (_mode == 1) return;
        _nextPosition = GameManager.Instance.PlayerReference.transform.position;
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
        SetBehaviourValues(false, false, true, 0, 0.0f, CondTimerConfused);
        _clip = AudioStorage.Instance.StandardEnemySound(EAudios.EnemyConfuse);
        _audiosource.PlayOneShot(_clip, 1.0f);
        _mode = 3;
        _resetTimer = _resetTimerRef;
        _timer = _confusedDuration;
        transform.LookAt(GameManager.Instance.PlayerReference.transform.position);
        Debug.Log("Acabo de ver al Jugador");

    }
    protected void CondTimerConfused()
    {
        _timer -= Time.deltaTime;

        if (!_watchingPlayer)
        {
            _resetTimer -= Time.deltaTime;
            if (_resetTimer < 0)
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
        SetBehaviourValues(false, false, true, 0, 0.0f, CondTimer);
        _clip = AudioStorage.Instance.StandardEnemySound(EAudios.EnemyChecking);
        _audiosource.PlayOneShot(_clip, 1.0f);
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
        SetBehaviourValues(false, false, true, 0, 0.0f, CondTimer);
        _clip = AudioStorage.Instance.StandardEnemySound(EAudios.EnemyConfuse);
        _audiosource.PlayOneShot(_clip, 1.0f);
        _timer = 2.1f;
        _mode = 5;
        _animator.SetTrigger("isHearing");
        _agent.SetDestination(_nextPosition);
        transform.LookAt(_nextPosition);
        PreMovement = MovFollowPosition;
        NextMovement = MovFollowPosition;
     Debug.Log("Escuche algo");
    }
    protected void MovStunned() // Persigue al Jugador
    {
        SetBehaviourValues(false, false, false, 0, 0.0f, CondTimer);
        _clip = AudioStorage.Instance.StandardEnemySound(EAudios.EnemyHurt);
        _audiosource.PlayOneShot(_clip, 1.0f);
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
    public void PlayAudioWalk()
    {
        _clip = AudioStorage.Instance.StandardEnemySound(EAudios.EnemyWalk);
        _audiosource.PlayOneShot(_clip, 0.025f);
    }
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
