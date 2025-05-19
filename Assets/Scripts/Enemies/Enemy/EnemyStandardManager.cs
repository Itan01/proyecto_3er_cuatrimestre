using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandardManager : EntityMonobehaviour
{
    private EnemyPattern _scriptPattern;
    private EnemyFollowTarget _scriptFollowTarget;
    private EnemyStandardManager _manager;
    private ControllerAnimator _scriptAnimator;
    [SerializeField] bool _canFollowTarget;
    [SerializeField] bool _canMovPattern;
    [SerializeField] private Transform[] _positionSequence;
    [SerializeField] GameObject _questionMark;
    [SerializeField] private Vector3 _dir= new Vector3();
    private float _speed = 3.5f;
    [SerializeField] private int _mode = 0;
    private Vector3 _mainPosition;
    protected override void Start()
    {

        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        GameMode();
    }
    protected override void FixedUpdate()
    {
        if (_mode == 0) MoveToMainPosition();
        Move();
    }
    protected override void GetScripts()
    {
        _mainPosition = transform.position;
        _manager = GetComponent<EnemyStandardManager>();
        _scriptAnimator = new ControllerAnimator(_animator);
        if (_canFollowTarget)
        {
            _scriptFollowTarget = new EnemyFollowTarget(_manager, transform, _scriptAnimator);
            _mode = 2;
            _scriptFollowTarget.Reset();
        }
    
        if (_canMovPattern)
        {
            _scriptPattern = new EnemyPattern(_positionSequence, transform, _scriptAnimator);
            _mode = 1;
        }
           
    }
    private void GameMode()
    {
        if (_mode == 1 && _canMovPattern)
        {
            _speed = 3.0f;
            _questionMark.SetActive(false);
            _dir = _scriptPattern.CheckIfHasArrive();
        }
        else if (_mode == 2 && _canFollowTarget)
        {
            _dir = _scriptFollowTarget.GetDirection();
        }
        else
        {
            _speed = 3.0f;
            _mode = 0; 
        }
    }
    private void Move()
    {
        if (_speed > 0.0f) 
            _animator.SetBool("isMoving",true);

        transform.LookAt(_dir);
        _rb.MovePosition(transform.position + _dir.normalized * _speed * Time.fixedDeltaTime);
    }
    public void SetMode(int Mode)
    {
        _mode = Mode;
    }
    public void SetTarget(Transform Target)
    {
        _scriptFollowTarget.SetTargetToFollow(Target);
        _speed = 5.0f;
    }
    protected virtual void MoveToMainPosition()
    {
        _dir = _mainPosition-transform.position;
        if (_dir.magnitude <= 0.2)
        {
            transform.position = _mainPosition;
            _animator.SetBool("isMoving", false);
            _speed = 0.0f;
        }
    }


    private void OnCollisionEnter(Collision Entity)
    {
        if (Entity.gameObject.TryGetComponent<PlayerManager>(out PlayerManager Entityscript))
        {
            Entityscript.SetDeathAnimation();
            _scriptFollowTarget.Reset();
        }
        if (Entity.gameObject.TryGetComponent<AbstractSound>(out AbstractSound SoundScript))
        {
            _speed = 3.0f;
            _questionMark.SetActive(true);
            _scriptFollowTarget.SetPostionToFollow(SoundScript.GetStartPoint());
            SetMode(2);
        }
    }
}

