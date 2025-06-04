using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SetCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider Player)
    {
        if (Player.TryGetComponent<PlayerManager>(out PlayerManager script))
        {
            if(script.GetCaptured()) return;
            GameManager.Instance.RespawnReference=transform.position;
        }
    }
}
