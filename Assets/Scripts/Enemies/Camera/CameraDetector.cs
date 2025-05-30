using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    private CameraObstacleController _CameraObstacleScript;

    private void Start()
    {
        _CameraObstacleScript = GetComponentInParent<CameraObstacleController>();
    }

    void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _CameraObstacleScript.SetTarget(true);
            Debug.Log("ON");
        }
    }

    void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            //Script.GetComponent<PlayerShootingGun>().ShootEnable(true);
            _CameraObstacleScript.SetTarget(false);
          Debug.Log("OFF");

        }
    }
}
