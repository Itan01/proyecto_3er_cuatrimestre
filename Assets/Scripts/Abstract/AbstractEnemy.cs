using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Timers;
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
    protected bool _isRunning = false, _questionBool;
    protected int _questionIndex;
    public delegate void Setter();
    protected Setter _movement;
    protected Setter _previousmovement;
    protected Setter _nextmovement;
    protected Setter _gamemode;
    protected override void Awake()
    {
        GetComponentInParent<RoomManager>().AddToList(this);
        _confusedDuration = _confusedDurationRef;
    }
    protected override void Start()
    {
        RoomManager Room = GetComponentInParent<RoomManager>();
        Room.DetPlayer += MoveFollowToPlayerPosition;
        Room.FindPlayer += MoveTimerTarget;
        base.Start();
        _questionMark = GetComponentInChildren<QuestionMarkManager>();
        SetMode(MoveBasePath);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!_activate) return;
        base.Update();
        _movement();
    }
    protected override void FixedUpdate()
    {
    }

    protected virtual void OnCollisionEnter(Collision Entity)
    {
        if (Entity.gameObject.GetComponent<PlayerManager>())
        {
            GameManager.Instance.ResetGameplay();
            SetMode(MoveResetPath);
            _nextPosition = _startPosition;
        }
    }

    public void EnterConfusedState()
    {
        SetMode(MoveConfused);
    }
    public void Respawn()
    {
        Debug.Log("Respawn");
        SetMode(MoveBasePath);
        transform.position = _startPosition;
    }
    #region InternalFunctions
    protected void SetMode(Setter TypeOfMovement)
    {
        _gamemode = TypeOfMovement;
        _gamemode();
        _questionMark.Setting(_questionBool, _questionIndex);
        _animator.SetBool("isMoving", _isMoving);
        _animator.SetBool("isRunning", _isRunning);
    }
    protected override void GetScripts()
    {
        _agent = GetComponent<NavMeshAgent>();
        _startPosition = transform.position;
    }
    #endregion
    #region TypeOfMovement
    protected virtual void MoveResetPath()
    {
    }
    protected virtual void MoveFollowTarget() // Persigue al Jugador
    {
        _isMoving = true;
        _isRunning = true;
        _agent.speed = _runSpeed;
        _questionBool = true;
        _questionIndex = 1;
        _mode = 1;
        transform.LookAt(_nextPosition);
        _previousmovement = MoveFollowTarget;
        _movement = ConditionMoveFollowTarget;
        _nextmovement = MoveFollowTarget;
        _agent.destination = _nextPosition;
        Debug.Log("Mirando Al Jugador");
    }
    protected virtual void MoveTimerTarget() // Persigue al Jugador
    {
        SetMode(MoveFollowTarget);
    }
    protected void ConditionMoveFollowTarget()
    {
        _nextPosition = GameManager.Instance.PlayerReference.transform.position;
        _agent.destination = _nextPosition;

    }
    protected void MoveFollowSound() // Persigue al lugar donde se genero el Sonido
    {
        _isMoving = true;
        _isRunning = false;
        _agent.speed = _baseSpeed;
        _questionBool = true;
        _questionIndex = 0;
        _mode = 2;
        _previousmovement = MoveFollowSound;
        _movement = ConditionToReachPosition;
        _nextmovement = MoveLooking;
        _agent.destination = _nextPosition;
        transform.LookAt(_nextPosition);
        Debug.Log("Yendo a donde escucho");

    }
    protected void MoveFollowToPlayerPosition() // Persigue al lugar donde se genero el Sonido
    {
        if (_mode == 1) return;
        _nextPosition= GameManager.Instance.PlayerReference.transform.position;
        MoveFollowSound();
        Debug.Log("Yendo a la posibleUbicacion del Player");

    }

    protected void ConditionToReachPosition()
    {
        if (_agent.remainingDistance < _shortDistance)
        {
            SetMode(_nextmovement);
        }
    }

    protected virtual void MoveBasePath() // patron normal (Esta en distintos scripts "StandEnemy""StandardEnemy")
    {
    }
    protected void MoveConfused()// Confundido (ve al player) es decir, se queda quieto en el lugar pero NO LO BUSCA, esto es solo por vision, no por radiustohear
    {
        _isMoving = false;
        _isRunning = false;
        _agent.speed = 0.0f;
        _questionBool = true;
        _questionIndex = 0;
        _mode = 3;
        _resetTimer = _resetTimerRef;
        _timer = _confusedDuration;
        _movement = ConditionsMoveConfused;
        transform.LookAt(GameManager.Instance.PlayerReference.transform.position);
        AudioStorage.Instance.EnemyConfusedSound();
    }
    protected void ConditionsMoveConfused()
    {
        _timer -= Time.deltaTime;
       
        if (!_watchingPlayer)
        {
            _resetTimer -= Time.deltaTime;
            if (_resetTimer <0)
                SetMode(_previousmovement);
        }
        if (_timer < 0.0f)
        {
            SetMode(MoveFollowTarget);
        }
    }
    protected void MoveLooking()
    {
        _isMoving = false;
        _isRunning = false;
        _agent.speed = 0.0f;
        _questionBool = true;
        _questionIndex = 0;
        _mode = 4;
        _timer = 2.5f;
        _previousmovement = MoveBasePath;
        _movement = ConditionToFinishTimer;
        _nextmovement = MoveBasePath;
        _animator.SetTrigger("isLooking");
        AudioStorage.Instance.EnemyConfusedSound();
        Debug.Log("Viendo alrededor");
    }
    protected void ConditionToFinishTimer()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0.0f)
        {
            SetMode(_nextmovement);
        }
    }

    protected void MoveStartHearing() // Persigue al Jugador
    {
        _isMoving = false;
        _isRunning = false;
        _agent.speed = 0.0f;
        _questionBool = true;
        _questionIndex = 0;
        _timer = 2.1f;
        _animator.SetTrigger("isHearing");
        _mode = 5;
        _previousmovement = MoveBasePath;
        _movement = ConditionToFinishTimer;
        _nextmovement = MoveFollowSound;
        AudioStorage.Instance.EnemyConfusedSound();
    }
    protected virtual void MoveStunned() // Persigue al Jugador
    {
        _isMoving = false;
        _isRunning = false;
        _agent.speed = 0.0f;
        _questionBool = false;
        _questionIndex = 0;
        _mode = 6;
        _timer = 2.45f;
        _animator.SetTrigger("Stun");
        _movement = ConditionToFinishTimer;
        _nextmovement = MoveBasePath;
        AudioStorage.Instance.EnemyTensionSound();
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
            SetMode(MoveFollowSound);
        else if (State == 3)
            SetMode(MoveConfused);
        else if (State == 5)
            SetMode(MoveStartHearing);
        else
            SetMode(MoveBasePath);
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
    #endregion
    public void IIteraction(bool PlayerShootIt)
    {
        if (PlayerShootIt)
        {
            Debug.Log("HIII");
            SetMode(MoveStunned);
        }
    }
    public void ForceRepath()
    {
        if (!_agent.enabled) return;
        _agent.ResetPath(); 
        _agent.SetDestination(_nextPosition); 
    }

}
