using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm_Camera
{
   
    private ICameraObstacle _currentState = null;
    public void SetStartBehaviour(ICameraObstacle Behaviour)
    {
        _currentState = Behaviour;
    }

    public void VirtualUpdate()
    {
        _currentState.Execute();
    }
    public void VirtualFixedUpdate()
    {
        _currentState.FixedExecute();
    }
    public void SetNewBehaviour(ECameraBehaviours Behaviour)
    {
        if (_currentState.GetBehaviour(Behaviour, out ICameraObstacle NewState))
        {
            _currentState.Exit();
            _currentState = NewState;
            _currentState.Enter();
        }
    }
}
