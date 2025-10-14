using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    private Camera_Obstacle _CameraObstacleScript;
    private void Awake()
    {
        _CameraObstacleScript = GetComponentInParent<Camera_Obstacle>();
        _CameraObstacleScript.SetCamera(transform);
    }

    void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _CameraObstacleScript.SetTarget = true;
        }
    }

    void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _CameraObstacleScript.SetTarget = false;
        }
    }
}
