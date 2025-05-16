using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    private PlayerShootingGun _shootingGunScript;
    private CameraObstacleController _CameraObstacleScript;

    private void Start()
    {
        _CameraObstacleScript = GetComponentInParent<CameraObstacleController>();
    }

    void OnTriggerEnter(Collider Player)
    {
        if (Player.TryGetComponent<PlayerManager>(out PlayerManager Script))
        {
            // Script.GetComponent<PlayerShootingGun>().ShootEnable(false);
            _CameraObstacleScript.SetTarget(Script.GetHipsPosition());
            Debug.Log("OFF");
        }
    }

    void OnTriggerExit(Collider Player)
    {
        if (Player.TryGetComponent<PlayerManager>(out PlayerManager Script))
        {
            //Script.GetComponent<PlayerShootingGun>().ShootEnable(true);
            _CameraObstacleScript.SetTarget(null);
            //Debug.Log("ON");

        }
    }
}
