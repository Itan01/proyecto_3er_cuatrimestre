using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class s_AbstractEnemy : EntityMonobehaviour
{
    private QuestionMarkManager _scriptQM;
    private NavMeshAgent _agent;
    private Vector3 _nextPosition;
    Action VirtualMovementUpdate;
    protected override void Awake()
    {
        throw new System.NotImplementedException();
    }
    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
        _scriptQM =GetComponentInChildren<QuestionMarkManager>();
    }
    protected override void Update()
    {
        base.Update();
        VirtualMovementUpdate();
    }

    protected override void FixedUpdate()
    {

    }

    public void SetBehaviourValues(bool isMoving, bool isRunning, bool QMState, int QMindex, float Speed)
    {
        _animator.SetBool("isMoving", isMoving);
        _animator.SetBool("isRunning", isRunning);
        _scriptQM.SetMark(QMState,QMindex);
        _agent.speed= Speed;
    }
    public void SetMovement(Action Movement)
    {

    }
    public void NewMode()
    {

    }
    public void SetAgentDestination(Vector3 Destination) 
    {
        _agent.SetDestination(Destination);
    }
    public NavMeshAgent GetAgent()
    {
        return _agent;
    }

    public Vector3 GetNextPosition()
    {
        return _nextPosition;
    }

}
