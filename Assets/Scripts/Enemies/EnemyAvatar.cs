using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAvatar : MonoBehaviour
{
    private AbstractEnemy _enemyScript;
    private NavMeshAgent _agent;

    private void Start()
    {
        _enemyScript = GetComponentInParent<AbstractEnemy>();
        _agent = GetComponentInParent<NavMeshAgent>();
    }

    public void HearSound()
    {
        _enemyScript.SetSpeed(0.0f);
    }
    public void StopHearing()
    {
        _enemyScript.SetSpeed(3.5f);
    }
}
