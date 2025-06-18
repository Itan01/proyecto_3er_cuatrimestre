using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
public abstract class AbstractEnemy : EntityMonobehaviour
{
    protected NavMeshAgent _agent;
    protected QuestionMarkManager _questionMark;
    protected float _baseSpeed = 3.5f, _runSpeed = 7.5f, _shortDistance = 0.25f;
    [SerializeField] protected float _timer = 0.0f;
    [SerializeField] protected int _mode = 0;
    [SerializeField] protected Transform _facingStartPosition;
    [SerializeField] protected Vector3 _nextPosition, _startPosition;
    protected float _confusedDuration = 1.0f; // Este es el tiempo de confusión donde cree haber visto al player
    protected float _searchDuration = 20.0f; // El tiempo que busca al player luego de que este salga del RadiusToHear
    protected bool _isRunning = false, _questionBool, _watchingPlayer=false;
    protected int _questionIndex;
    private EnemyVision _vision;

    public delegate void SetMove();
    protected SetMove _movement;
    protected override void Awake()
    {
    }
    protected override void Start()
    {
        base.Start();
        GameManager.Instance.RegisterEnemy(this);
        _questionMark = GetComponentInChildren<QuestionMarkManager>();
        _vision = GetComponentInChildren<EnemyVision>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (_timer != 0)
            TimerToSearch();
        if (_agent.remainingDistance <= _shortDistance  )
            NextMovement();
        if (_mode == 1)
            _agent.destination = GameManager.Instance.PlayerReference.transform.position;
        if (_mode == -1)
            transform.LookAt(_facingStartPosition);
    }
    protected override void FixedUpdate()
    {
    }

    protected void OnCollisionEnter(Collision Entity)
    {
        if (Entity.gameObject.TryGetComponent<PlayerManager>(out PlayerManager Entityscript))
        {
            if (!Entityscript.IsPlayerDeath())
            {
                Entityscript.SetDeathAnimation();
                    SetMode(MoveResettingPath);
            }
        }
    }

    public void EnterConfusedState()
    {
        SetMode(MoveConfused);
    }
    public void Respawn()
    {
        transform.position = _startPosition;
        _nextPosition = _startPosition;
        SetMode(MoveResettingPath);
        _agent.ResetPath();
        _animator.SetBool("isMoving", false);
        _animator.SetBool("isRunning", false);
    }
    #region InternalFunctions
    protected void SetMode(SetMove TypeOfMovement)
    {
        _movement = TypeOfMovement;
        GameMode();
    }
    protected void GameMode()
    {
        transform.LookAt(Vector3.Slerp(transform.forward,_nextPosition, 10.0f));
        _movement();
        _animator.SetBool("isRunning", _isRunning);
        _animator.SetBool("isMoving",_isMoving );
        _questionMark.Setting(_questionBool, _questionIndex);
    }
    protected void CheckConfusedState()
    {
        if (_watchingPlayer)
            SetMode(MoveFollowTarget);
        else
            SetMode(MoveResettingPath);
    }
    protected virtual void NextMovement()
    {
        if (_mode == 2)
        {
            SetMode(MoveLooking);
        }

    }
    protected override void GetScripts()
    {
        _agent = GetComponent<NavMeshAgent>();
        _startPosition = transform.position;
    }
    protected void TimerToSearch()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = 0.0f;
            if (_mode == 3) // Solo en el Estado de Confusion Puede cambiar a otro valor que no sea ResetPath
                CheckConfusedState();
            else
                SetMode(MoveResettingPath);
        } // Se Acabo el timer cambia de modo (Probablemente vuelva al 0)

    }
    #endregion
    #region TypeOfMovement
    protected void MoveStandPosition() // Persigue al Jugador
    {
        _mode = -1;
        _isMoving = false;
        _isRunning = false;
        _questionBool = false;
        _questionIndex = 1;
        _agent.speed = 0.0f;
        _nextPosition = _startPosition;
        transform.LookAt( _facingStartPosition);
    }
    protected void MoveFollowTarget() // Persigue al Jugador
    {
        _mode = 1;
        _isMoving = true;
        _isRunning = true;
        _questionBool = true;
        _questionIndex = 1;
        _agent.speed = _runSpeed;
        _nextPosition = GameManager.Instance.PlayerReference.transform.position;
        _agent.destination = _nextPosition;
        Debug.Log("Mirando Al Jugador");
    }
    protected void MoveFollowSound() // Persigue al lugar donde se genero el Sonido
    {
        _mode = 2;
        _isMoving = true;
        _isRunning = false;
        _questionBool = true;
        _questionIndex = 0;
        _agent.speed = _baseSpeed;
        _agent.destination = _nextPosition;
      Debug.Log("Yendo a donde escucho");
    }

    protected virtual void MoveResettingPath() // patron normal (Esta en distintos scripts "StandEnemy""StandardEnemy")
    {
    }
    protected void MoveConfused()// Confundido (ve al player) es decir, se queda quieto en el lugar pero NO LO BUSCA, esto es solo por vision, no por radiustohear
    {
        _mode = 3;
        _questionBool = true;
        _questionIndex = 0;
        _isMoving = false;
        _isRunning = false;
        _agent.speed = 0.0f;
        _timer=_confusedDuration;
        transform.LookAt(GameManager.Instance.PlayerReference.transform.position);
        Debug.Log("Vi al jugador");
    }
    protected void MoveLooking()
    {
        _mode = 4;
        _agent.speed = 0.0f;
        _questionBool = true;
        _questionIndex = 0;
        _isMoving = false;
        _isRunning = false;
        _animator.SetTrigger("isLooking");
        Debug.Log("Viendo alrededor");
    }

    protected void MoveStartHearing() // Persigue al Jugador
    {
        _animator.SetTrigger("isHearing");
        _mode = 5;
        _isMoving = false;
        _isRunning = false;
        _questionBool = true;
        _questionIndex = 0;
        _agent.speed = 0.0f; ;
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
        else if (State == 5)
            SetMode(MoveStartHearing);
        else
            SetMode(MoveResettingPath);
    }
    public int GetMode()
    {
        return _mode;
    }
    public void SetPosition(Vector3 pos)
    {
        pos.y =transform.position.y;
        _nextPosition = pos;
    }

    public void WatchingPlayer(bool State)
    {
        _watchingPlayer=State;
    }
    #endregion
}
