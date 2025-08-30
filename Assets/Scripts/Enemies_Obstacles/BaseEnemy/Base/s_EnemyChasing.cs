using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_EnemyChasing : MonoBehaviour, IEnemyTreeNode
{
    private s_AbstractEnemy _scriptParent;
    private Transform _target;
    private PlayerManager _player;
    private float _speed = 10.0f;
    public s_EnemyChasing(s_AbstractEnemy ScriptEnemy)
    {
        _scriptParent = ScriptEnemy;
        _target = GameManager.Instance.PlayerReference.transform;
    }
    public void SetBehaviour()
    {
        _scriptParent.SetBehaviourValues(true, true, true, 1, _speed, Movement);
        _scriptParent.SetConditionAndMovement(Condition, Movement);
        Debug.Log("Persiguiendo Al Jugador");
    }
    public void Condition()
    {

    }
    public void Movement()
    {
        _scriptParent.SetAgentDestination(_target.position);
    }
}
