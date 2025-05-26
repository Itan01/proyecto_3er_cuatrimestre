using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionToHear : MonoBehaviour
{
    private EnemyStandardManager _scriptManager;
    private void Start()
    {
        _scriptManager = GetComponentInParent<EnemyStandardManager>();
    }
    void OnTriggerEnter(Collider Player)
    {
        if (Player.TryGetComponent<PlayerManager>(out PlayerManager ScriptPlayer))
        {
            if (ScriptPlayer.IsPlayerMoving())
            {
                Debug.Log("Hearing");
                _scriptManager.SetTarget(Player.transform);
                _scriptManager.SetMode(2);
            }
        }
    }

}
