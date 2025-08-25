using System;
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
    protected bool _isMakingNoise=false, _isCoughing = false;
    protected float coughTimer, coughTimerReference = 2f, deathTimer = 10f, deathTimerReference = 10f, _noiseTimer, _noiseTimerRef = 0.5f;
    [SerializeField] Transform HeadReference;
    protected bool  _isThisPlayer = false; 
    protected bool _isMoving = false;
    protected bool _isDeath = false;
    protected bool _isCrouching = false;
    protected AudioSource _audiosource;
    protected AudioClip _clip;
    Action VirtualUpdate;
    protected abstract void Awake();
    protected virtual void Start()
    {
        GetComponents();
    }
    protected virtual void Update()
    {
        if (VirtualUpdate != null)
        VirtualUpdate();
    }
    protected abstract void FixedUpdate();
    protected virtual void GetComponents()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _animator = GetComponentInChildren<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _boxCollider = GetComponent<BoxCollider>();
        _audiosource=GetComponent<AudioSource>();
    }
    public void SetSoundInvoker(bool State)
    {
        _isMakingNoise = State;
        if (State)
            VirtualUpdate += MakeNoiseTimer;
        else
            VirtualUpdate -= MakeNoiseTimer;
    }
    public void GameObjectSoundInvoker(GameObject Sound)
    {
        _noise = Sound;
    }

    protected virtual void MakeNoiseTimer()
    {
        if(_isMoving && !_isCrouching)
        {
            _noiseTimer -= Time.deltaTime;
            if (_noiseTimer <= 0)
            {
                Vector3 RandomPosition = new Vector3(UnityEngine.Random.Range(-2.0f, 1.0f + 1), 1.0f, UnityEngine.Random.Range(-2.0f, 1.0f + 1)).normalized * 2;
                RandomPosition += transform.position;
                Vector3 Orientation = RandomPosition - transform.position;
                var Sound = Instantiate(_noise, RandomPosition, Quaternion.identity);
                AbstractSound Script = Sound.GetComponent<AbstractSound>();
                Script.SetDirection(Orientation + transform.up, 4.0f, 1.0f);
                if (_isThisPlayer)
                {
                    Script.SetIfPlayerSummoned(true);
                }
                _noiseTimer = _noiseTimerRef;
            }
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
            VirtualUpdate -= CoughTimerSubstract;
        }
        else
            VirtualUpdate += CoughTimerSubstract;

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
