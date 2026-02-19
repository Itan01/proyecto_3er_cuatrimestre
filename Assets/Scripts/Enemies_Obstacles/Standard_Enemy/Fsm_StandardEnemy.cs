public class Fsm_StandardEnemy
{
    private IStandardEnemy _currentState = null;
    public void SetStartBehaviour(IStandardEnemy Behaviour)
    {
        _currentState = Behaviour;
        _currentState.Enter();
    }

    public void VirtualUpdate()
    {
        _currentState.Execute();
    }
    public void VirtualFixedUpdate()
    {
        _currentState.FixedExecute();
    }
    public void SetNewBehaviour(EStandardEnemyBehaviours Behaviour)
    {
        if (_currentState.GetBehaviour(Behaviour, out IStandardEnemy NewState))
        {
            _currentState.Exit();
            _currentState = NewState;
            _currentState.Enter();
        }
    }
}