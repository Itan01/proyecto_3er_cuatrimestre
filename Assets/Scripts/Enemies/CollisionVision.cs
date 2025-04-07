using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionVision : MonoBehaviour
{
    private PlayerSetCheckpoint _scriptCheckpoint;
    private void Start()
    {
    }
    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            _scriptCheckpoint = player.GetComponent<PlayerSetCheckpoint>();
            _scriptCheckpoint.MoveToCheckPoint();
        }
    }
}
