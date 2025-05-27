using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class AbstractEnemy : EntityMonobehaviour
{
    protected NavMeshAgent _agent;
    [SerializeField] protected int _mode = 0;
    [SerializeField] protected Transform _facingStartPosition;
    [SerializeField] protected Vector3 _nextPosition, _startPosition;
    protected override void Awake()
    {
    }
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

    }
    protected override void GetScripts()
    {
        _agent = GetComponent<NavMeshAgent>();
        _startPosition = transform.position;
    }
    protected void Move()
    {
        _agent.SetDestination(_nextPosition);
    }

    public void SetMode(int Mode)
    {
        _mode = Mode;
    }
    protected virtual void GameMode()
    {
    }
    protected void OnCollisionEnter(Collision Entity)
    {
        if (Entity.gameObject.TryGetComponent<PlayerManager>(out PlayerManager Entityscript))
        {
            Entityscript.SetDeathAnimation();
            _nextPosition = _startPosition;
            SetMode(0);
        }
        if (Entity.gameObject.TryGetComponent<AbstractSound>(out AbstractSound SoundScript))
        {
            if (SoundScript.GetIfPlayerSummoned())
            {
                _nextPosition = SoundScript.GetStartPoint();
                SetMode(2);
            }

        }
    }
}
