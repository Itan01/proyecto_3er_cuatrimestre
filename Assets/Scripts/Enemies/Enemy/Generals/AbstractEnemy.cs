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
    [SerializeField] protected float _confusedDuration = 2f; // Este es el tiempo de confusión donde cree haber visto al player
    protected float _confusedTimer = 0f;
    [SerializeField] protected float _searchDuration = 3.5f; // El tiempo que busca al player luego de que este salga del RadiusToHear
    protected float _searchTimer = 0f;
    private EnemyVision _vision;
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
        if (this is StandEnemyManager) // Enemigo guardia 
        {
            // Solo se mueve si busca o persigue (porque sino se quedaba caminando del lugar y no volvía a su posición inicial)
            if (_mode != 1 && _mode != 2)
            {
                _agent.ResetPath();
                return;
            }
        }
        else
        {
            // Empieza el patrullaje normal de los enemigos normalitos
            if (_mode == 0)
            {
                _agent.ResetPath();
                return;
            }
        }

        _agent.destination = _nextPosition;

        if (_agent.remainingDistance <= 0.4f && _timer == 0.0f)
        {
            if (_mode != 1 && _mode != 2 && _mode != 3)
            {
                SetMode(0); // Solo resetea si patrulla
            }
        }
    }

    public void SetMode(int Mode)
    {
        _mode = Mode;

        if (Mode == 2) // Buscar
        {
            _searchTimer = _searchDuration; 
        }
    }
    protected void Timer()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0.0f)
        {
            _timer = 0.0f;
        }
    }

    protected void GameMode()
    {
        switch (_mode)
        {
            case 1: // Persiguiendo 
                _questionMark.Setting(true, 1);
                _animator.SetBool("isRunning", true);
                _animator.SetBool("isMoving", true);
                _agent.speed = _runSpeed;
                _nextPosition = GameManager.Instance.PlayerReference.transform.position;
                transform.LookAt(_nextPosition);
                break;

            case 2: // Buscando (caso de impacto con sonidos o player entrando al radio de sonido del enemigo). O sea, busca al personaje/sonido
                _searchTimer -= Time.deltaTime;
                _questionMark.Setting(true, 0);
                _animator.SetBool("isMoving", true);
                _animator.SetBool("isRunning", false);
                _agent.speed = _baseSpeed;

                // Si el player no esta el el radio, vuelve a patrullar si se acaba el tiempo 
                {
                    _nextPosition = _startPosition;

                    // Se fija si llego a la posicion inicial, y si lo hizo, lo pone mirando a su miradainicial(? no se como le decíamos
                    if (Vector3.Distance(transform.position, _startPosition) < 0.2f)
                    {
                        transform.rotation = Quaternion.LookRotation((_facingStartPosition.position - transform.position).normalized);
                        SetMode(0);
                    }
                }

                break;

            case 3: // Confundido (ve al player) es decir, se queda quieto en el lugar pero NO LO BUSCA, esto es solo por vision, no por radiustohear
                _confusedTimer -= Time.deltaTime;
                _questionMark.Setting(true, 0);
                _animator.SetBool("isMoving", false);
                _animator.SetBool("isRunning", false);
                if (_confusedTimer <= 0f)
                {
                    if (_vision.IsPlayerVisible())
                    {
                        SetMode(1); // comienza a perseguir
                    }
                    else
                    {
                        SetMode(0); // vuelve a patrullar
                    }
                }
                break;

            default: // Patrullando
                _questionMark.Setting(false, 0);
                _agent.speed = _runSpeed / 2;
                _animator.SetBool("isRunning", false);

                _nextPosition = _startPosition;

                if (this is StandEnemyManager) 
                {
                    float distance = Vector3.Distance(transform.position, _startPosition);
                    bool shouldMove = distance > 0.4f;

                    if (shouldMove)
                    {
                        _animator.SetBool("isMoving", true);
                    }
                    else
                    {
                        _animator.SetBool("isMoving", false);
                        transform.LookAt(_facingStartPosition.position); 
                    }

                    return;
                }

                if ((_startPosition - transform.position).magnitude < 0.4f)
                {
                    transform.LookAt(_facingStartPosition.position);
                    _animator.SetBool("isMoving", false);
                }
                else
                {
                    _animator.SetBool("isMoving", true);
                }
                break;
        }
    }

    protected void OnCollisionEnter(Collision Entity)
    {
        if (Entity.gameObject.TryGetComponent<PlayerManager>(out PlayerManager Entityscript))
        {
            if (!Entityscript.IsPlayerDeath())
            {
                Entityscript.SetDeathAnimation();

                if (_mode == 3) // Si estaba confundido
                {
                    SetMode(1); // Comienza a perseguir
                }
                else
                {
                    SetMode(0); // Vuelve a patrullar
                }

                _nextPosition = _startPosition;
                _timer = 1.0f;
            }


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
    public void EnterConfusedState()
    {
        _confusedTimer = _confusedDuration;
        SetMode(3);
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