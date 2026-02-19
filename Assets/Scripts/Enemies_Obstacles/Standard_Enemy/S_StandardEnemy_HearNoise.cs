using System;
using UnityEngine;
public class S_StandardEnemy_HearNoise : Cons_StandardEnemy
{
    private float _timer=2.0f;
    public S_StandardEnemy_HearNoise(Fsm_StandardEnemy Fsm) : base(Fsm)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _agent.isStopped = true;
        _animator.SetBool("isMoving", false);
        _animator.SetTrigger("isHearing");
        _entity.State = EStandardEnemyBehaviours.Hear;
    }
    public override void Execute()
    {
       _timer -= Time.deltaTime;
        if (_timer > 0) return;
        _fsm.SetNewBehaviour(EStandardEnemyBehaviours.MoveToPosition);
    }
    public override void Exit()
    {
        _timer = 2.0f;
        _agent.isStopped = false;
       
    }
}