using System.Collections;
using UnityEngine;

public class RoombaEnemy : AbstractEnemy
{
    [SerializeField] private float _explodeDistance = 1.0f;
    [SerializeField] private float _timeBeforeExplosion = 2.0f;
    [SerializeField] private RoomManager _roomManager;

    [SerializeField] private GameObject _explosionParticlesPrefab;
    [SerializeField] private GameObject _soundExplosionPrefab;
    [SerializeField] private int _amountOfSounds = 3;
    [SerializeField] private float _soundForce = 5f;
    [SerializeField] private float _spread = 1.5f;

    private bool _isOpening = false;
    private bool _hasStartedWalking = false;
    private bool _isExploding = false;

    protected override void Start()
    {
        base.Start();
        _animator.SetBool("Open_Anim", false);
        _animator.SetBool("Walk_Anim", false);
    }

    protected override void Update()
    {
        if (!_activate || _isExploding) return;

        base.Update();

        if (_hasStartedWalking)
        {
            float distance = Vector3.Distance(transform.position, GameManager.Instance.PlayerReference.transform.position);
            if (distance <= _explodeDistance)
            {
                StartCoroutine(ExplodeAfterDelay());
            }
        }
    }

    protected override void MoveFollowTarget()
    {
        _mode = 1;
        _isMoving = true;
        _isRunning = false;
        _questionBool = false;
        _agent.speed = _baseSpeed;

        _nextPosition = GameManager.Instance.PlayerReference.transform.position;
        transform.LookAt(_nextPosition);
        _agent.destination = _nextPosition;
    }

    public override void SetActivate(bool state)
    {
        base.SetActivate(state);

        if (state)
        {
            if (!_isOpening)
            {
                _isOpening = true;
                StartCoroutine(OpenAndThenWalk());
            }
        }
        else
        {
            SetMode(MoveIdle);
        }
    }

    private IEnumerator OpenAndThenWalk()
    {
        _animator.SetBool("Open_Anim", true);
        yield return new WaitForSeconds(1.5f);
        _animator.SetBool("Open_Anim", false);
        _animator.SetBool("Walk_Anim", true);
        SetMode(MoveFollowTarget);
        _hasStartedWalking = true;
    }

    private void MoveIdle()
    {
        _mode = 0;
        _isMoving = false;
        _isRunning = false;
        _questionBool = false;
        _agent.speed = 0.0f;
        _animator.SetBool("Walk_Anim", false);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (_isExploding) return;

        if (collision.gameObject.GetComponent<PlayerManager>())
        {
            _isExploding = true;

            _agent.isStopped = true;
            _animator.SetBool("Walk_Anim", false);
            _hasStartedWalking = false;
            StartCoroutine(ExplodeAfterDelay());
        }
    }

    private IEnumerator ExplodeAfterDelay()
    {
        _isExploding = true;
        yield return new WaitForSeconds(_timeBeforeExplosion);
        Explode();
    }

    private void Explode()
    {
        // particulas explosion
        if (_explosionParticlesPrefab)
        {
            Instantiate(_explosionParticlesPrefab, transform.position, Quaternion.identity);
        }

        // tirar sonido 
        for (int i = 0; i < _amountOfSounds; i++)
        {
            if (_soundExplosionPrefab)
            {
                GameObject sound = Instantiate(_soundExplosionPrefab, transform.position, Quaternion.identity);

                // la direccion que tira los sonidos (vertical)
                Vector3 randomDir = Vector3.up + new Vector3(
                    Random.Range(-_spread, _spread),
                    0f,
                    Random.Range(-_spread, _spread)
                ).normalized;

                if (sound.TryGetComponent(out Rigidbody rb))
                {
                    rb.AddForce(randomDir * _soundForce, ForceMode.Impulse);
                }
            }
        }
        _roomManager.RemoveEnemy(this);
        Destroy(gameObject);
    }

    protected override void MoveBasePath() { }
}