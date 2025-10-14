using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Camera_Detector : MonoBehaviour
{
    private Camera_Obstacle _camera;
    private Cons_Raycast _rayCast;
    [SerializeField]private SO_Layers _layers;
    Action VirtualUpdate;
    private void Start()
    {
        _rayCast= new Cons_Raycast(40.0f, _layers._everything);
        _camera = GetComponentInParent<Camera_Obstacle>();
    }

    private void Update()
    {
        VirtualUpdate?.Invoke();
    }

    void OnTriggerEnter(Collider Entity)
    {
        if (Entity.GetComponent<PlayerManager>())
        {
            VirtualUpdate = CheckView;
        }
    }

    void OnTriggerExit(Collider Entity)
    {
        if (Entity.GetComponent<PlayerManager>())
        {
            VirtualUpdate = null;
        }
    }
    private void CheckView()
    {
        Transform HipPosition= GameManager.Instance.PlayerReference.GetHipsPosition();
        Transform HeadPosition= GameManager.Instance.PlayerReference.GetHeadPosition();
    }
}
