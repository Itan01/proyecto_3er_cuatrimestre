using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeDetector : MonoBehaviour
{
    private void Start()
    {
        GetComponentInParent<RoomManager>().SetSmoke(gameObject);
        gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider Player)
    {
        if (Player.TryGetComponent(out EntityMonobehaviour Script))
        {
            Script.CoughState(true);
            
        }
    }

    void OnTriggerExit(Collider Player)
    {
    if (Player.TryGetComponent(out EntityMonobehaviour Script))
    {
        Script.CoughState(false);
    }
    }
}