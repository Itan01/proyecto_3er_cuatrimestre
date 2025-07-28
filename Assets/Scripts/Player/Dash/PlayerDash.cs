using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash
{
    private LayerMask _mask;
    private Transform _modelTransform;
    private Ray _ray;
    private RaycastHit _hits;
    private float _distance=5.0f;
    public PlayerDash(Transform ModelTransform)
    {
        _modelTransform=ModelTransform;
        _mask = LayerManager.Instance.GetLayerMask(EnumLayers.ObstacleMask);
    }

    public void Dash()
    {
        _ray = new Ray(_modelTransform.position + new Vector3(0.0f,1.0f,0.0f),_modelTransform.forward);
        if(Physics.Raycast(_ray,out _hits,_distance,_mask, QueryTriggerInteraction.Ignore))
        {
            Debug.Log("HI");
            GameManager.Instance.PlayerReference.transform.position = _hits.point;
        }
    }
}
