using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class EnemyStandardManager : EntityMonobehaviour
{
    private EnemyPattern _scriptPattern;
    private EnemyFollowTarget _scriptFollowTarget;
    private EnemyStandardManager _manager;
    private ControlAnimator _scriptAnimator;
    [SerializeField] bool _canFollowTarget;
    [SerializeField] bool _canMovPattern;
    [SerializeField] private Transform[] _positionSequence;
    private Vector3 _dir= new Vector3();
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
        _scriptAnimator = new ControlAnimator(_animator);
        if (_canFollowTarget)
            _scriptFollowTarget = new EnemyFollowTarget(_manager, transform, _scriptAnimator);
        if (_canMovPattern)
            _scriptPattern = new EnemyPattern(_positionSequence, transform, _scriptAnimator);
    }
    private void GameMode()
    {
        if (_mode == 0)
        {
            Debug.Log("Modo 1");
            _dir = _scriptPattern.CheckIfHasArrive();
        }
        if (_mode == 1)
        {
            Debug.Log("Modo 2");
            _dir = _scriptFollowTarget.GetDirection();
        }
    }
    private void Move()
    {
        _dir.y = transform.position.y;
        _rb.MovePosition(transform.position + _dir.normalized * _speed * Time.fixedDeltaTime);
    }
    public void SetMode(int Mode)
    {
        _mode = Mode;
        if (_mode == 0)
        {
            _speed = 3.0f;
            _scriptAnimator.SetBoolAnimator("isRunning", false);
        }

        if (_mode == 1)
        {
            _scriptAnimator.SetBoolAnimator("isRunning", true);
            _speed = 5.0f;
        }
    }
    public void SetTarget(Transform Target)
    {
        _scriptFollowTarget.SetTargetToFollow(Target);
    }

    private void OnCollisionEnter(Collision Player)
    {
        if (Player.gameObject.TryGetComponent<PlayerManager>(out PlayerManager script))
        {
            script.SetDeathAnimation();
            _scriptFollowTarget.Reset();
        }
    }
}

