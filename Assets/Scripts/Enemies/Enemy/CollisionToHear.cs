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
        if ((_player.transform.position - transform.position).magnitude <= 6.5f)
        {
            if (_player.IsPlayerMoving() && !_player.IsPlayerCrouching() && !_player.IsPlayerDeath())
            {
                _scriptManager.SetPosition(_player.transform.position);
                if(_scriptManager.GetMode() != 1)
                _scriptManager.SetMode(2);
            }
        }
    }

    private void OnTriggerEnter(Collider Sound)
    {
        if (Sound.TryGetComponent<AbstractSound>(out AbstractSound ScriptSound))
        {
            ScriptSound.SetTarget(transform, 7.5f);
        }
    }

}
