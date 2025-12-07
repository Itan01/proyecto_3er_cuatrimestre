using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider Player)
    {
        if (Player.TryGetComponent<PlayerManager>(out PlayerManager script))
        {
            if(script.IsCaptured) return;
            LVLManager.Instance.Respawn=transform.position;
        }
    }
}
