using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityMonobehaviour : MonoBehaviour
{
    protected Rigidbody _rb;
    protected Animator _animator;
    protected CapsuleCollider _capsuleCollider;
    protected BoxCollider _boxCollider;
    protected GameObject _noise;
    protected bool coughCondition = false;
    protected float coughTimer = 2f, coughTimerReference = 2f, deathTimer = 10f, deathTimerReference = 10f;
    [SerializeField] Transform HeadReference;
    

    [SerializeField] protected bool _makeNoise, _summonedByPlayer = false;
    [SerializeField] protected float _noiseTimer = 0.5f, _noiseTimerRef = 0.5f;
    [SerializeField] protected bool _isMoving = false;
    protected bool _isDeath = false;
    [SerializeField] protected bool _isCrouching = false;
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        GetComponents();
        GetScripts();
    }
    protected virtual void Update()
    {
        if (_makeNoise && _isMoving && !_isCrouching)
        {
            MakeNoiseTimer();
        }
        if(coughCondition)
        {
            CoughTimerSubstract();
        }
    }
    protected virtual void FixedUpdate()
    {

    }
    protected virtual void GetComponents()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _animator = GetComponentInChildren<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _boxCollider = GetComponent<BoxCollider>();
    }
    protected virtual void GetScripts()
    {

    }
    public void SetSoundInvoker(bool State)
    {
        _makeNoise = State;
    }
    public void GameObjectSoundInvoker(GameObject Sound)
    {
        _noise = Sound;
    }

    protected virtual void MakeNoiseTimer()
    {
        _noiseTimer -= Time.deltaTime;
        if (_noiseTimer <= 0)
        {
            Vector3 RandomPosition = new Vector3(Random.Range(-2.0f, 1.0f + 1), 1.0f, Random.Range(-2.0f, 1.0f + 1)).normalized * 2;
            //Debug.Log(RandomPosition);
            RandomPosition += transform.position;
            Vector3 Orientation = RandomPosition - transform.position;
            var Sound = Instantiate(_noise, RandomPosition, Quaternion.identity);
            AbstractSound Script = Sound.GetComponent<AbstractSound>();
            Script.SetDirection(Orientation + transform.up, 4.0f, 1.0f);
            if (_summonedByPlayer)
            {
                Script.SetIfPlayerSummoned(true);
            }
            _noiseTimer = _noiseTimerRef;
        }
    }


    public bool IsPlayerMoving()
    {
        return _isMoving;
    }
    public bool IsPlayerCrouching()
    {
        return _isCrouching;
    }
    public bool IsPlayerDeath()
    {
        return _isDeath;
    }

    public void CoughState(bool coughState)
    {
        if (!coughState)
        {
            coughTimer = coughTimerReference;
        }
        coughCondition = coughState;

    }

    protected void CoughTimerSubstract()
    {
        coughTimer -= Time.deltaTime;
        if (coughTimer <= 0)
        {
            coughTimer = coughTimerReference;
            GameManager.Instance.ResetGameplay();
        }
    }
}
