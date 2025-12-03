using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cons_Raycast
{
    private LayerMask _mask;
    private float _maxDistance;
    private RaycastHit _hits;
    public Cons_Raycast(float MaxDistance, LayerMask Mask)
    {
        _maxDistance = MaxDistance;
        _mask = Mask;
    }
    public bool CheckerComponent<T>( Vector3 StartPosition,Vector3 Direction) where T : Component
    {
        if (Physics.Raycast(StartPosition, Direction, out _hits, _maxDistance, _mask, QueryTriggerInteraction.Ignore))
        {
            if (_hits.collider.GetComponent<T>())
            {
               Debug.Log("Verdadero");
                return true;
            }

        }
        Debug.Log("Falso");
        return false;
    }
    public bool Checker(Vector3 StartPosition, Vector3 Direction)
    {
        if (Physics.Raycast(StartPosition, Direction, out _hits, _maxDistance, _mask, QueryTriggerInteraction.Ignore))
        {
            Debug.Log("Verdadero");
            return true;

        }       Debug.Log("Falso");
        return false;
    }
}
