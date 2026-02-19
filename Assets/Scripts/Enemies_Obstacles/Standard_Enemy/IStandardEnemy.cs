public interface IStandardEnemy
{
    public void Enter();
    public void Execute();
    public void FixedExecute();
    public void Exit();
    public void AddBehaviour(EStandardEnemyBehaviours BehaviourName, IStandardEnemy State);
    public bool GetBehaviour(EStandardEnemyBehaviours Key, out IStandardEnemy State);
}