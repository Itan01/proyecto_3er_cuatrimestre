using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class s_EnemyHearing : MonoBehaviour, IEnemyTreeNode
{
    private s_AbstractEnemy _scriptParent;
    private NavMeshAgent _agent;
    private float _timer = 2.1f, _refTimer = 2.1f;
    private Vector3 _pos;

    public s_EnemyHearing(s_AbstractEnemy ScriptEnemy)
    {
        _scriptParent = ScriptEnemy;
    }

    public void Movement()
    {
        if (_timer < 0.0f)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _timer = _refTimer;
            _scriptParent.NewMode();
        }
    }

    public void SetBehaviour()
    {
        _scriptParent.SetBehaviourValues(true, false, true, 0, 0.0f);
    }

}
