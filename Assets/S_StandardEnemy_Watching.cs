using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StandardEnemy_Watching : Cons_StandardEnemy
{
    private ITreeNode _root;
    private float _timer, _timerRef= 5.0f;
    private float _timerOff;//  Tiempo de cambio, cuando deja de ver al jugador, tardara un 1seg en Cambiar de modo
    private readonly float _timerOffRef=1.0f;
    public S_StandardEnemy_Watching(Fsm_StandardEnemy Fsm) : base(Fsm)
    {
        _timerOff = _timerOffRef;
        _timer = _timerRef;
        ActionNode SubstractTimerLoseVision = new ActionNode(SubstractCounterOff);
        ActionNode ChangeToPatrolMode = new ActionNode(SetPreviousMode);
        ActionNode ChangeToChaseMode = new ActionNode(SetChaseMode);
        ActionNode SubtractTimerToChangeMode = new ActionNode(SubstractCounter);


        QuestionNode WatchingTimer = new QuestionNode(ActualTimer, ChangeToChaseMode, SubtractTimerToChangeMode);
        QuestionNode LoseVisionTimer = new QuestionNode(LoseVisionState, ChangeToPatrolMode, SubstractTimerLoseVision);
        QuestionNode WatchingPlayer = new QuestionNode(AmIWatchingPlayer, WatchingTimer, LoseVisionTimer);

        _root = WatchingPlayer;
    }
    public override void Enter()
    {
        base.Enter();
        _animator.SetBool("isMoving", false);
        _entity.State = EStandardEnemyBehaviours.Watching;
        _agent.isStopped = true;
    }
    public override void Execute()
    {
        _root.Execute();
    }
    public override void Exit()
    {
        base.Exit();
        _agent.isStopped = false;
        _timerOff = _timerOffRef;
        _timer = _timerRef;
    }
    #region Questions

    private bool AmIWatchingPlayer()
    {
       return _entity.CheckVision();
    }
    private bool ActualTimer()
    {
        _timerOff = _timerOffRef;
        return _timer <= 0;
    }
    private bool LoseVisionState()
    {
        _timer = _timerRef;
        return _timerOff <= 0;
    }
    #endregion
    #region Actions
    private void SetPreviousMode()
    {
        if(_entity.PreviousState == EStandardEnemyBehaviours.MoveToPosition)
            _fsm.SetNewBehaviour(EStandardEnemyBehaviours.MoveToPosition);
        else
        _fsm.SetNewBehaviour(EStandardEnemyBehaviours.Patrol);
    }
    private void SetChaseMode()
    {
        _fsm.SetNewBehaviour(EStandardEnemyBehaviours.Chase);
    }
    private void SubstractCounter()
    {
        _timer -= Time.deltaTime;
    }
    private void SubstractCounterOff()
    {
        _timerOff -= Time.deltaTime;
    }
    #endregion
}