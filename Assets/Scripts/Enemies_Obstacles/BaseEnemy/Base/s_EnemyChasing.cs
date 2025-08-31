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
        _player = GameManager.Instance.PlayerReference;
        _target = _player.transform;
    }
    public void SetBehaviour()
    {
        _scriptParent.SetBehaviourValues(true, true, true, 1, _speed);
        Debug.Log("Persiguiendo Al Jugador");
    }
    public void Movement()
    {
        if (_player.IsPlayerDeath())
        {
            _scriptParent.NewMode();
        }
         _scriptParent.SetAgentDestination(_target.position);
    }
}
