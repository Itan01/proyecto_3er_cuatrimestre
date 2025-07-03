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
    protected float _baseSpeed = 3.5f, _runSpeed = 7.0F, _shortDistance = 0.25f;
    [SerializeField] protected float _timer = 0.0f;
    [SerializeField] protected int _mode = 0;
    [SerializeField] protected Transform _facingStartPosition;
    protected Vector3 _nextPosition, _startPosition;
    [SerializeField] protected float _confusedDuration;
    protected float _confusedDurationRef = 5.0f; // Este es el tiempo de confusión donde cree haber visto al player
    protected float _searchDuration = 20.0f; // El tiempo que busca al player luego de que este salga del RadiusToHear
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
        base.Start();
        _questionMark = GetComponentInChildren<QuestionMarkManager>();
        SetMode(MoveBasePath);
    }

    // Update is called once per frame
    protected override void Update()
    {
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
            SetMode(MoveBasePath);
        }
    }

    public void EnterConfusedState()
    {
        SetMode(MoveConfused);
    }
    public void Respawn()
    {
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
        _movement = ConditionsMoveFollowSound;
        _nextmovement = MoveLooking;
        _agent.destination = _nextPosition;
        transform.LookAt(_nextPosition);
        Debug.Log("Yendo a donde escucho");

    }

    protected void ConditionsMoveFollowSound()
    {
        if (_agent.remainingDistance < 0.25f)
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
        _timer = _confusedDuration;
        _movement = ConditionsMoveConfused;
        AudioStorage.Instance.EnemyConfusedSound();
    }
    protected void ConditionsMoveConfused()
    {
        _timer -= Time.deltaTime;
        transform.LookAt(GameManager.Instance.PlayerReference.transform.position);
        if (!_watchingPlayer)
            SetMode(_previousmovement);
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
        _movement = ConditionsMoveLooking;
        _nextmovement = MoveBasePath;
        _animator.SetTrigger("isLooking");
        AudioStorage.Instance.EnemyConfusedSound();
        Debug.Log("Viendo alrededor");
    }
    protected void ConditionsMoveLooking()
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
        _movement = ConditionMoveStartHearing;
        _nextmovement = MoveFollowSound;
        AudioStorage.Instance.EnemyConfusedSound();
    }
    protected void ConditionMoveStartHearing()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0.0f)
        {
            SetMode(_nextmovement);
        }
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
        _movement = ConditionStunned;
        _nextmovement = MoveBasePath;
        Debug.Log("Stunned");
    }
    protected void ConditionStunned()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0.0f)
        {
            SetMode(_nextmovement);
        }
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
}
