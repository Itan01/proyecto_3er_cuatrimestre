using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cons_StandardEnemy : IStandardEnemy
{
    private Dictionary<EStandardEnemyBehaviours, IStandardEnemy> _behaviours = new Dictionary<EStandardEnemyBehaviours, IStandardEnemy>();
    protected Fsm_StandardEnemy _fsm;
    protected NavMeshAgent _agent;
    protected S_StandardEnemy _entity;
    protected Transform _myTransform;
    protected Animator _animator;
    protected Color _colorVision;
    protected AudioClip _enterClip;
    protected AudioClip _clip;
    public Cons_StandardEnemy(Fsm_StandardEnemy Fsm)
    {
        _fsm = Fsm;
    }
    public Cons_StandardEnemy Entity(S_StandardEnemy Entity)
    {
        _entity=Entity;
        return this;
    }
    public Cons_StandardEnemy Agent(NavMeshAgent Agent)
    {
        _agent=Agent;
        _myTransform = _agent.transform;
        return this;
    }
    public Cons_StandardEnemy Animator(Animator Animator)
    {
        _animator = Animator;
        return this;
    }
    public Cons_StandardEnemy DATA(DATA_FEEDBACK_STATE DATA)
    {
        if (DATA.ColorVision != null)_colorVision = DATA.ColorVision;
        if (DATA.EnterClip != null) _enterClip = DATA.EnterClip;
        if (DATA.Clip != null) _clip = DATA.Clip;
        return this;
    }
    public virtual void AddBehaviour(EStandardEnemyBehaviours BehaviourName, IStandardEnemy State)
    {
        if (!_behaviours.ContainsKey(BehaviourName))
        {
            _behaviours.Add(BehaviourName, State);
        }
    }
    public virtual void Enter()
    {
        if (_colorVision != null) _entity.SetColorVision(_colorVision);
        if (_enterClip != null) _entity.PlayAudio(_enterClip);
        if (_entity.State != _entity.PreviousState)
        {
            _entity.PreviousState = _entity.State;
        }

    }

    public virtual void Execute()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void FixedExecute()
    {

    }
    public virtual bool GetBehaviour(EStandardEnemyBehaviours Key, out IStandardEnemy State)
    {

        if (_behaviours.ContainsKey(Key))
        {
            State = _behaviours[Key];
            return true;
        }
        else
        {
            State = null;
            return false;
        }

    }
}