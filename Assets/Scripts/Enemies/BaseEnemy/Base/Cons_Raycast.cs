using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cons_Raycast
{
    private LayerMask _mask;
    private float _maxDistance;
    private RaycastHit _hits;
    private Vector3 _startPosition, _dir;
    private Transform _player;
    public Cons_Raycast(Vector3 StartPosition, Vector3 Direction, float MaxDistance, LayerMask Mask)
    {
        _startPosition = StartPosition;
        _dir = Direction;
        _maxDistance = MaxDistance;
        _mask = Mask;
    }
    public bool Checker<T>()
    {
        if (Physics.Raycast(_startPosition, _dir, out _hits, _maxDistance, _mask, QueryTriggerInteraction.Ignore))
        {
            //if(_hits.collider.GetComponent<T>())
                return true;
        }
            return false;         
    }
}
