using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            EventManager.Trigger(EEvents.Reset);
        }
    }
}
