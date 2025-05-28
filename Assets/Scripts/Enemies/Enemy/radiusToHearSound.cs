using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RadiusToHearSound : MonoBehaviour
{
    private AbstractEnemy _scriptManager;
    private PlayerManager _player;
    private void Start()
    {
        _player=GameManager.Instance.PlayerReference;
        _scriptManager = GetComponentInParent<AbstractEnemy>();
    }

    private void Update()
    {

        if ((_player.transform.position - transform.position).magnitude <= 5.0f)
        {
            if(_player.IsPlayerMoving() && !_player.isPlayerCrouching())
            {
                _scriptManager.SetMode(1);
            }
        }
    }
}
