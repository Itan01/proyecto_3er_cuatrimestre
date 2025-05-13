using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class EnemyStandardManager : EntityMonobehaviour
{
    private EnemyPattern _scriptPattern;
    private EnemyFollowTarget _scriptFollowTarget;
    private EnemyStandardManager _manager;
    [SerializeField] bool _canFollowTarget;
    [SerializeField] bool _canMovPattern;
    [SerializeField] private Transform[] _positionSequence;
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
        if (_canFollowTarget)
            _scriptFollowTarget = new EnemyFollowTarget(_manager, transform);
        if (_canMovPattern)
            _scriptPattern = new EnemyPattern(_positionSequence, transform);
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
    }
    public void SetTarget(Transform Target)
    {
        _scriptFollowTarget.SetTargetToFollow(Target);
    }

    private void OnCollisionEnter(Collision Player)
    {
        if (Player.gameObject.TryGetComponent<PlayerManager>(out PlayerManager script))
        {
            script.MovetoCheckPoint();
            _scriptFollowTarget.Reset();
        }
    }
}

