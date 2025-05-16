using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] GameObject _questionMark;
    private Vector3 _dir= new Vector3();
    private float _speed = 3.5f;
    [SerializeField] private int _mode = 0;
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
            _speed = 3.0f;
            _questionMark.SetActive(false);
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
            SetMode(1);
        }
    }
}

