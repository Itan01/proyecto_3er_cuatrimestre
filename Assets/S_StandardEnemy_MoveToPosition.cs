using System;
using System.Reflection;
using UnityEngine;

public class S_StandardEnemy_MoveToPosition : Cons_StandardEnemy
{
    private Vector3 _desirePosition;
    private ITreeNode _root;
    public S_StandardEnemy_MoveToPosition(Fsm_StandardEnemy Fsm) : base(Fsm)
    {
        ActionNode ChangeToWatchMode = new ActionNode(SetWatchMode);
        ActionNode ChangeToSearchMode = new ActionNode(SetSearchMode);
        ActionNode Arrive_To_waypoint = new ActionNode(ArriveToWaypoint);

        QuestionNode HearingPlayer = new QuestionNode(IsHearingPlayer, ChangeToSearchMode, Arrive_To_waypoint);
        QuestionNode WatchingPlayer = new QuestionNode(IsWatchingPlayer, ChangeToWatchMode, HearingPlayer);
        _root = WatchingPlayer;
    }
    public override void Enter()
    {
        base.Enter();
        _desirePosition = _entity.DesirePos;
        _entity.State = EStandardEnemyBehaviours.MoveToPosition;
        _animator.SetBool("isMoving",true);
        _agent.SetDestination(_desirePosition);
    }
    public override void Execute()
    {
        _root.Execute();
    }
    #region Questions
    private bool IsWatchingPlayer()
    {
        return _entity.CheckVision();
    }
    private bool IsHearingPlayer()
    {
        return _entity.CheckDistanceToHear();
    }

    #endregion
    #region Actions

    private void SetWatchMode()
    {
        _fsm.SetNewBehaviour(EStandardEnemyBehaviours.Watching);
    }
    private void SetSearchMode()
    {
        _desirePosition = GameManager.Instance.PlayerReference.transform.position;
        _agent.SetDestination(_desirePosition);
    }
    private void ArriveToWaypoint()
    {
        if (Vector3.SqrMagnitude(_desirePosition - _myTransform.position) > 0.25f) return;
        _fsm.SetNewBehaviour(EStandardEnemyBehaviours.Search);
    }
    #endregion
}