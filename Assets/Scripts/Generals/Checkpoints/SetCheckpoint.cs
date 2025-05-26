using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SetCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            GameManager.Instance.RespawnReference=transform.position;
        }
    }
}
