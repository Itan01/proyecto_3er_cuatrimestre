using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionVision : MonoBehaviour
{
    private PlayerSetCheckpoint _scriptCheckpoint;
    private void Start()
    {
        _scriptCheckpoint = GetComponent<PlayerSetCheckpoint>();
    }
    void OnTriggerEnter(Collider vision)
    {
        if (vision.gameObject.CompareTag("Vision"))
        {
            _scriptCheckpoint.MoveToCheckPoint();
        }
    }
}
