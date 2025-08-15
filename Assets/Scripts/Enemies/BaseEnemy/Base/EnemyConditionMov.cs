using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConditionMov
{
    private IEnemyTreeNode _nextMov;
    private IEnemyTreeNode _prevMov;
    private float _distance, _minDistance;
    public EnemyConditionMov(IEnemyTreeNode NextMovement, IEnemyTreeNode PreviousMovement)
    {
        _nextMov=NextMovement;
        _prevMov = PreviousMovement;
    }
    public void CheckCondition()
    {
        if (_distance<= _minDistance)
        {
            _nextMov.Behaviour();
        }
    }
}
