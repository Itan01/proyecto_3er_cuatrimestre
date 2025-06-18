using System.Collections;
using System.Collections.Generic;
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
        if (_player.IsPlayerDeath() || !_player.IsPlayerMoving() || _player.IsPlayerCrouching()) return;
        if ((_player.transform.position - transform.position).magnitude <= 6.5f)
        {
            if (_scriptManager.GetMode() != 1)
            {
                _scriptManager.SetPosition(_player.transform.position);
                _scriptManager.SetModeByIndex(5);

            }

        }
    }
}
