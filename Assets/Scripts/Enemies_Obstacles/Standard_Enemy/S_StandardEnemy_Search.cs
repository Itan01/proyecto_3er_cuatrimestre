using UnityEngine;
using System;
public class S_StandardEnemy_Search : Cons_StandardEnemy
{
    private float _timer, _timerRef = 3.5f;
    public S_StandardEnemy_Search(Fsm_StandardEnemy Fsm) : base(Fsm)
    {
        _timer = _timerRef;
    }
    public override void Enter()
    {
        base.Enter();
        _animator.SetTrigger("isLooking");
        _animator.SetBool("isMoving",false);
        _agent.isStopped = true;
        _entity.State = EStandardEnemyBehaviours.Search;
        _entity.ShowMark(true, EMarkEnemyState.QuestionMark);
    }
    public override void Execute()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0) return;
        _fsm.SetNewBehaviour(EStandardEnemyBehaviours.Patrol);
    }
    public override void Exit()
    {
        _timer = _timerRef;
        _agent.isStopped = false;
        _entity.ShowMark(false, EMarkEnemyState.AlertMark);
    }
}