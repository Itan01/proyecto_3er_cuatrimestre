using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetCheckpoint : MonoBehaviour
{
    [SerializeField] private Vector3 _checkpointPosition;
    private GameObject Player;
    void Start()
    {
        if (!Player)
        {
            Player = GameObject.Find("Player");
        }
            _checkpointPosition = new Vector3(-8, 1, -4);
    }

    public void ChangeCheckPoint(Vector3 PositionCheckpoint)
    {
        _checkpointPosition = PositionCheckpoint;
    }

    public void MoveToCheckPoint()
    {
        Player.transform.position = _checkpointPosition;
    }

    private void OnTriggerEnter(Collider checkpoint)
    {
        if (checkpoint.gameObject.CompareTag("Checkpoint"))
        {
            ChangeCheckPoint(checkpoint.transform.position);
        }
    }
}
