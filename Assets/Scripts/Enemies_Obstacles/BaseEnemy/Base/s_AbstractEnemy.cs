using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class s_AbstractEnemy : EntityMonobehaviour
{
    private s_EnemyChasing _scriptChasing;
    private QuestionMarkManager _scriptQM;
    private NavMeshAgent _agent;

    private Action VirtualConditionUpdate, VirtualMovementUpdate;
    protected override void Awake()
    {
        throw new System.NotImplementedException();
    }
    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
        _scriptQM =GetComponentInChildren<QuestionMarkManager>();
        _scriptChasing = new s_EnemyChasing(this);
    }
    protected override void Update()
    {
        base.Update();
        VirtualConditionUpdate();

    }

    protected override void FixedUpdate()
    {
        VirtualMovementUpdate();
    }

    public void SetBehaviourValues(bool isMoving, bool isRunning, bool QMState, int QMindex, float Speed, Action NewMovement)
    {
        _animator.SetBool("isMoving", isMoving);
        _animator.SetBool("isRunning", isRunning);
        _agent.speed= Speed;
    }
    public void SetConditionAndMovement(Action NewCondition, Action Movement)
    {

    }
    public void SetAgentDestination(Vector3 Destination) 
    {
        _agent.SetDestination(Destination);
    }

}
