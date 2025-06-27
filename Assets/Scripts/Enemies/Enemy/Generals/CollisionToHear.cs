using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CollisionToHear : MonoBehaviour
{
    private AbstractEnemy _scriptManager;
    private PlayerManager _player;
    private void Start()
    {
        _player = GameManager.Instance.PlayerReference;
        _scriptManager = GetComponentInParent<AbstractEnemy>();
    }

    private void Update()
    {

        
        if ((_player.transform.position - transform.position).magnitude <= 7.5f)
        {
            if (_player.IsPlayerDeath() || !_player.IsPlayerMoving() || _player.IsPlayerCrouching()) return;

            if (_scriptManager.GetMode() < 1)
            {
                _scriptManager.SetPosition(_player.transform.position);
                _scriptManager.SetModeByIndex(2);
            }
        }
    }
}
