using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAction : MonoBehaviour, IEnemyTreeNode
{
    Action _action;
    public EnemyAction(Action action)
    {
        _action = action;
    }

    public void Condition()
    {
        throw new NotImplementedException();
    }

    public void Movement()
    {
        throw new NotImplementedException();
    }

    public void SetBehaviour()
    {
        _action();
    }
}
