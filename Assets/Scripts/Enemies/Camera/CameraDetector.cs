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
        if (Player.gameObject.layer == 27)
        {
            Player.GetComponent<PlayerManager>().DisableShoot(true) ;
            _CameraObstacleScript.SetDoors(false);
            _CameraObstacleScript.SetTarget(Player.transform);
            //Debug.Log("OFF");
        }
    }

    void OnTriggerExit(Collider Player)
    {
        if (Player.gameObject.layer == 27)
        {
            Player.GetComponent<PlayerManager>().DisableShoot(true);
            _CameraObstacleScript.SetDoors(true);
            _CameraObstacleScript.SetTarget(null);
            //Debug.Log("ON");
        }
    }
}
