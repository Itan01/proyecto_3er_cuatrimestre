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
    protected float _baseSpeed = 3.5f, _runSpeed = 7.5f, _shortDistance = 0.2f;
    [SerializeField] protected float _timer = 0.0f;
    [SerializeField] protected int _mode = 0;
    [SerializeField] protected Transform _facingStartPosition;
    protected Vector3 _nextPosition, _startPosition;
    protected float _confusedDuration = 1.0f; // Este es el tiempo de confusión donde cree haber visto al player
    protected float _searchDuration = 7.0f; // El tiempo que busca al player luego de que este salga del RadiusToHear
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
        if (_timer != 0)
            TimerToSearch();
        if (_agent.remainingDistance <= _shortDistance)
            NextMovement();
        if (_mode == 1)
            _agent.destination = GameManager.Instance.PlayerReference.transform.position;
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
            _animator.SetTrigger("isLooking");
            SetMode(MoveResettingPath);
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
        if (_timer < 0) // Se Acabo el timer cambia de modo (Probablemente vuelva al 0)
        {
            _timer = 0.0f;
            if (_mode != 3) // Solo en el Estado de Confusion Puede cambiar a otro valor que no sea ResetPath
                SetMode(MoveResettingPath);
            else
                CheckConfusedState();
        }
    }
    #endregion
    #region TypeOfMovement
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
       // Debug.Log("Mirando Al Jugador");
    }
    protected void MoveFollowSound() // Persigue al lugar donde se genero el Sonido
    {
        _animator.SetTrigger("isHearing");
        _mode = 2;
        _isMoving = true;
        _isRunning = false;
        _questionBool = true;
        _questionIndex = 0;
        _timer = _searchDuration;
        _agent.destination = _nextPosition;
       // Debug.Log("Yendo a donde escucho");
    }
    protected virtual void MoveResettingPath() // patron normal (Esta en distintos scripts "StandEnemy""StandardEnemy")
    {
    }
    protected void MoveConfused()// Confundido (ve al player) es decir, se queda quieto en el lugar pero NO LO BUSCA, esto es solo por vision, no por radiustohear
    {
        _mode = 3;
        _questionBool = true;
        _questionIndex = 0;
        _isMoving = true;
        _isRunning = false;
        _timer=_confusedDuration;
        //Debug.Log("Vi al jugador");
    }
    #endregion
    #region Set/Get Values

   public void SetSpeed(float Speed)
    {
        _agent.speed = Speed;
    }
    public void SetModeByIndex(int State)
    {
        if (State==2)
        {
            SetMode(MoveFollowSound);
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

    public void WatchingPlayer(bool State)
    {
        _watchingPlayer=State;
    }
    #endregion
}

// HOLA ITAN. Bueno, nada, si vas a modificar lo el sonido, dejé marcado en cada case 
// para lo que sirve cada uno. En teoría habría que hacer una layer como dijiste de particulas
// y hacer que el estado de busqueda se aplique también no solo al colisionar con el player 
// en el radiustohear, sino también que vaya a buscar al sonido. 
// En el caso del sonido del sonido crash que hacemos ahora, tendría que funcionar solo agregarlo
// y ya está, porque es muy similar a lo que pasa con el player ahora. 
// La verdad no sé si sirvió de algo los comentarios pero lo di todo



//if (Entity.gameObject.TryGetComponent<AbstractSound>(out AbstractSound SoundScript))
//{
//    if (SoundScript.GetIfPlayerSummoned() && _mode !=1)
//    {
//        _nextPosition = SoundScript.GetStartPoint();
//        SetMode(2);
//        _timer = 1.0f;
//    }
//    Destroy(SoundScript.gameObject);
//}

// Esto es lo que teníamos antes para cuando "atraia el sonido". lo dejé como comentario x las dudas