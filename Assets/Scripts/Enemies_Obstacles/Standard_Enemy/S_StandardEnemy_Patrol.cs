using System.Collections.Generic;
using UnityEngine;

internal class S_StandardEnemy_Patrol : Cons_StandardEnemy
{
    private List<Transform> _positions=new();
    private List<Transform> _positionsRef=new();
    private int _index=0;   
    private ITreeNode _root;
    public S_StandardEnemy_Patrol(Fsm_StandardEnemy Fsm) : base(Fsm)
    {
        ActionNode ChangeToWatchMode = new ActionNode(SetWatchMode);
        ActionNode ChangeToMovePosition = new ActionNode(SetMoveToPoint);
        ActionNode Arrive_To_waypoint = new ActionNode(ArriveToWaypoint);

        QuestionNode HearingPlayer = new QuestionNode(IsHearingPlayer, ChangeToMovePosition, Arrive_To_waypoint);
        QuestionNode WatchingPlayer = new QuestionNode(IsWatchingPlayer, ChangeToWatchMode, HearingPlayer);

        _root = WatchingPlayer;

    }
    public S_StandardEnemy_Patrol Positions(Transform[] Positions)
    {
        for (int i=0;i<Positions.Length ;i++)
        {
            _positions.Add(Positions[i]);
            _positionsRef.Add(Positions[i]);
        }
        if (_positions.Count == 0) Debug.Log("No position sets, Enemy wont Move");
        return this;    

    }
    public void AddPosition(Transform Reference)
    {
        _positions = _positionsRef;
        _positions.Insert(_index + 1, Reference);
    }
    public override void Enter()
    {
        base.Enter();
        _entity.State = EStandardEnemyBehaviours.Patrol;
        _animator.SetBool("isMoving",true);
        Vector3 Pos = _positions[_index].position;
        _agent.SetDestination(Pos);
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
    public void SetWaypoint(int Index)
    {
        _index = Index;
        Vector3 Pos = _positions[_index].position;
        _agent.SetDestination(Pos);
    }
    private void SetWatchMode()
    {
        _fsm.SetNewBehaviour(EStandardEnemyBehaviours.Watching);
    }
    private void SetMoveToPoint()
    {
        _entity.DesirePos = GameManager.Instance.PlayerReference.transform.position;
        _fsm.SetNewBehaviour(EStandardEnemyBehaviours.MoveToPosition);
    }
    private void ArriveToWaypoint()
    {
        if (Vector3.SqrMagnitude(_positions[_index].position - _myTransform.position) > 0.25f) return;
        _index++;
        if (_index >= _positions.Count) _index = 0;
        Vector3 Pos = _positions[_index].position;
        _agent.SetDestination(Pos);
    }
    #endregion
}