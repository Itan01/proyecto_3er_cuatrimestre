using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandardManager : EntityMonobehaviour
{
    private EnemyPattern _scriptPattern;
    private EnemyFollowTarget _scriptFollowTarget;
    private EnemyStandardManager _manager;
    private ControllerAnimator _scriptAnimator;
    [SerializeField] private Transform[] _positionSequence;
    [SerializeField] GameObject _questionMark;
    [SerializeField] private Vector3 _dir= new Vector3();
    private float _speed = 3.5f;
    [SerializeField] private int _mode = 1;
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
        Move();
    }
    protected override void GetScripts()
    {
        _manager = GetComponent<EnemyStandardManager>();
        _scriptAnimator = new ControllerAnimator(_animator);
        _scriptFollowTarget = new EnemyFollowTarget(_manager, transform, _animator);
        _scriptPattern = new EnemyPattern(_positionSequence, transform, _scriptAnimator);
        _mode = 1;

    }
    private void GameMode()
    {
        if (_mode == 1)
        {
            _speed = 3.0f;
            _questionMark.SetActive(false);
            _dir = _scriptPattern.CheckIfHasArrive();
        }
        if (_mode == 2)
        {
            _dir = _scriptFollowTarget.GetDirection();
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

