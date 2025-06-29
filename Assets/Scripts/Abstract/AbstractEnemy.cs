using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;
public abstract class AbstractEnemy : EntityMonobehaviour
{
    protected NavMeshAgent _agent;
    [SerializeField] protected bool _activate;
    protected QuestionMarkManager _questionMark;
    protected float _baseSpeed = 3.5f, _runSpeed = 10.0f, _shortDistance = 0.25f;
    [SerializeField] protected float _timer = 0.0f;
    [SerializeField] protected int _mode = 0;
    [SerializeField] protected Transform _facingStartPosition;
    protected Vector3 _nextPosition, _startPosition;
    protected float _confusedDuration = 1.0f; // Este es el tiempo de confusión donde cree haber visto al player
    protected float _searchDuration = 20.0f; // El tiempo que busca al player luego de que este salga del RadiusToHear
    [SerializeField] protected bool _watchingPlayer = false;
    protected bool _isRunning = false, _questionBool;
    protected int _questionIndex;
    public delegate void SetMove();
    protected SetMove _movement;
    protected override void Awake()
    {
        GetComponentInParent<RoomManager>().AddToList(this);
    }
    protected override void Start()
    {
        base.Start();

        _questionMark = GetComponentInChildren<QuestionMarkManager>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!_activate) return;
        base.Update();
        if (_timer != 0)
            TimerToSearch();
        if (_agent.remainingDistance <= _shortDistance && _timer ==0.0f)
            NextMovement();
        if (_mode == 1)
            _agent.destination = GameManager.Instance.PlayerReference.transform.position;
    }
    protected override void FixedUpdate()
    {
    }

    protected void OnCollisionEnter(Collision Entity)
    {
        if (Entity.gameObject.GetComponent<PlayerManager>())
        {
            GameManager.Instance.ResetGameplay();
            SetMode(MoveResettingPath);
        }
    }

    public void EnterConfusedState()
    {
        SetMode(MoveConfused);
    }
    public void Respawn()
    {
        SetMode(MoveResettingPath);
        transform.position = _startPosition;
    }
    #region InternalFunctions
    protected void SetMode(SetMove TypeOfMovement)
    {
        _movement = TypeOfMovement;
        GameMode();
    }
    protected void GameMode()
    {
        _movement();
        _animator.SetBool("isRunning", _isRunning);
        _animator.SetBool("isMoving",_isMoving );
        _questionMark.Setting(_questionBool, _questionIndex);
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
        }
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
        transform.LookAt(_nextPosition);
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
        transform.LookAt(_nextPosition);
        _timer = 1.0f;
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
        transform.LookAt(GameManager.Instance.PlayerReference.transform.position);
        StartCoroutine(CheckifWatchingPlayer());

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
        else if (State == 3)
            SetMode(MoveConfused);
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
    public void SetActivate(bool State)
    {
        _activate = State;
    }
    public bool GetActivate()
    {
        return _activate;
    }
    #endregion

    private IEnumerator CheckifWatchingPlayer()
    {
        yield return new WaitForSeconds(_confusedDuration);
        if (_watchingPlayer)
            SetMode(MoveFollowTarget);
        else
            SetMode(MoveResettingPath);
    }
}
