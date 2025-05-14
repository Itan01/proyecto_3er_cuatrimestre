using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint
{
    private Vector3 _checkpointPosition;
    private Transform _player;

    public PlayerCheckpoint(Transform PlayerTransform)
    {
        _player=PlayerTransform;
    }

    public void SetCheckpoint(Vector3 PositionCheckpoint)
    {
        _checkpointPosition = PositionCheckpoint;
    }

    public void Respawn()
    {
        _player.position = _checkpointPosition;
    }
}
