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
        if (Player.gameObject.CompareTag("Player"))
        {
            _shootingGunScript = Player.GetComponent<PlayerShootingGun>();
            _shootingGunScript.ShootEnable(false);
            _CameraObstacleScript.SetTarget(Player.transform);
            //Debug.Log("OFF");
        }
    }

    void OnTriggerExit(Collider Player)
    {
        if (Player.gameObject.CompareTag("Player"))
        {
            _shootingGunScript = Player.GetComponent<PlayerShootingGun>();
            _shootingGunScript.ShootEnable(true);
            _CameraObstacleScript.SetTarget(null);
            //Debug.Log("ON");

        }
    }
}
