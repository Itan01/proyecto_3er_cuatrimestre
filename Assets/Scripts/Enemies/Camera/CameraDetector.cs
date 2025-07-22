using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    private CameraObstacleController _CameraObstacleScript;
    [SerializeField] private List<AbstractEnemy> _enemies ;//No tener lista

    //Event
    private void Awake()
    {
        _CameraObstacleScript = GetComponentInParent<CameraObstacleController>();
        _CameraObstacleScript.SetCamera(transform);
    }
    private void Start()
    {

    }

    void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _CameraObstacleScript.SetTarget(true);
            //Lanzo evento con posicion (Ej: EventDetect(transform.position);
            foreach (AbstractEnemy enemy in _enemies)
            {
                if (enemy != null) 
                {
                    enemy.SetPosition(transform.position); 
                    enemy.SetModeByIndex(2);
                }
            }
            //_enemies.SetPosition(transform.position);
            //_enemies.SetModeByIndex(2);
           // Debug.Log("ON");
        }
    }

    void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            //Script.GetComponent<PlayerShootingGun>().ShootEnable(true);
            _CameraObstacleScript.SetTarget(false);
          //Debug.Log("OFF");

        }
    }
}
