using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionVision : MonoBehaviour
{
    private AbstractEnemy _scriptManager;
    private void Start()
    {
        _scriptManager = GetComponentInParent<AbstractEnemy>();
    }
    void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.layer == 27)
        {
            _scriptManager.SetMode(1);
        }
    }
}
