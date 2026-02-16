using UnityEngine;

public class S_StandardEnemy_Stunned : Cons_StandardEnemy
{
    private float _timer, _timerRef=3.0f;

    public S_StandardEnemy_Stunned(Fsm_StandardEnemy Fsm) : base(Fsm)
    {
        _timer = _timerRef;
    }
    public override void Enter()
    {
        base.Enter();
        _agent.isStopped = true;
        _animator.SetBool("isMoving", false);
        _animator.SetTrigger("Stun");
        _entity.State = EStandardEnemyBehaviours.Stunned;
        _agent.speed = Mathf.Clamp(_agent.speed + 0.1f,1.0f,5.0f);
        _entity.AddNewPos();
    }
    public override void Execute()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0) return;
        _fsm.SetNewBehaviour(EStandardEnemyBehaviours.Search);
    }
    public override void Exit()
    {
        _timer = _timerRef;
        _agent.isStopped = false;

    }
}