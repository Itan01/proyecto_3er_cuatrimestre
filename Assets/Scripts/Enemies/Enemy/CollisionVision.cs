using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionVision : MonoBehaviour
{
    private EnemyStandardManager _scriptManager;
    private void Start()
    {
        _scriptManager = GetComponentInParent<EnemyStandardManager>();
    }
    void OnTriggerStay(Collider Player)
    {
        if (Player.gameObject.layer == 27)
        {
            _scriptManager.SetTarget(Player.transform);
            _scriptManager.SetMode(2);
        }
    }
}
