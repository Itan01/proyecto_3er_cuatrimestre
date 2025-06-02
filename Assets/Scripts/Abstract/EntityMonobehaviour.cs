using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMonobehaviour : MonoBehaviour
{
    protected Rigidbody _rb;
    protected Animator _animator;
    protected CapsuleCollider _capsuleCollider;
    protected BoxCollider _boxCollider;
    protected GameObject _noise;
    [SerializeField] protected bool _makeNoise;
    protected float _noiseTimer=1.0f;
    [SerializeField] protected bool _isMoving = false;
    [SerializeField] protected bool _isDeath = false;
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

    }
    protected virtual void FixedUpdate()
    {

    }
    protected virtual void GetComponents()
    {
        _rb =GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _animator =GetComponentInChildren<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _boxCollider=GetComponent<BoxCollider>();
    }
    protected virtual void GetScripts()
    {

    }
    public void SetSoundInvoker(bool State)
    {
        _makeNoise=State;
    }
    public void GameObjectSoundInvoker(GameObject Sound)
    {
        _noise = Sound;
    }

    protected void MakeNoiseTimer()
    {
        _noiseTimer -= Time.deltaTime;
        if (_noiseTimer <= 0)
        {
            Vector3 RandomPosition= transform.position + new Vector3(Random.Range(-0.1f, 0.1f + 1), 0, Random.Range(-0.1f, 0.1f + 1))*5;
            Vector3 Orientation = RandomPosition - transform.position;
            var Sound = Instantiate(_noise, RandomPosition, Quaternion.identity);
            AbstractSound Script= Sound.GetComponent<AbstractSound>();
            Script.SetDirection(Orientation + transform.up,3.5f,1.0f);
            _noiseTimer = 1.0f;
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
}
