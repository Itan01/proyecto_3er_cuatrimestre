using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraObstacle
{
    public void Enter();
    public void Execute();
    public void FixedExecute();
    public void Exit();
    public void AddBehaviour(ECameraBehaviours BehaviourName, ICameraObstacle State);
    public bool GetBehaviour(ECameraBehaviours Key, out ICameraObstacle State);

}
