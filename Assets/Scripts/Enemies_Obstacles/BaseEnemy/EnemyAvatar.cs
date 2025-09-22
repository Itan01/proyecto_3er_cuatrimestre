using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAvatar : MonoBehaviour
{
    private AbstractEnemy _enemyScript;

    private void Start()
    {
        _enemyScript = GetComponentInParent<AbstractEnemy>();
    }

    public void ResettingPath()
    {
        _enemyScript.SetModeByIndex(0);
    }
    public void SearchSound()
    {
        _enemyScript.SetModeByIndex(2);
    }
    public void FollowTarget()
    {
        _enemyScript.SetModeByIndex(1);
    }
    public void PlayAudioWalk()
    {
        _enemyScript.PlayAudioWalk();
    }
}
