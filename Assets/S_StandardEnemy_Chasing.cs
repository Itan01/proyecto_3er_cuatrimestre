using System.Collections.Generic;
using UnityEngine;

public class S_StandardEnemy_Chasing : Cons_StandardEnemy
{
    private Transform _playerTransform;
    private float _multiplier =3.5f;
    public S_StandardEnemy_Chasing(Fsm_StandardEnemy Fsm) : base(Fsm)
    {
        _playerTransform = GameManager.Instance.PlayerReference.transform;
    }
    public override void Enter()
    {
        base.Enter();
        _animator.SetBool("isRunning",true);
        _entity.State = EStandardEnemyBehaviours.Chase;
        _agent.speed = _entity.Speed() * _multiplier;
        _entity.ShowMark(true, EMarkEnemyState.AlertMark);
    }
    public override void Execute()
    {
        _agent.SetDestination(_playerTransform.position);
    }
    public override void Exit()
    {
        _animator.SetBool("isRunning", false);
        _agent.speed= _entity.Speed();
        _entity.ShowMark(false, EMarkEnemyState.AlertMark);
    }
    #region Questions
    #endregion
    #region Actions
    #endregion
}