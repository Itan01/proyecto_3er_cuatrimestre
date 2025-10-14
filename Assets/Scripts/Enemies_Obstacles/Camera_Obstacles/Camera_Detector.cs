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
            Debug.Log("EnterPlayer");
            VirtualUpdate = CheckView;
        }
    }

    void OnTriggerExit(Collider Entity)
    {
        if (Entity.GetComponent<PlayerManager>())
        {
            Debug.Log("ExitPlayer");
            VirtualUpdate = null;
            _camera.SetTarget = false;
        }
    }
    private void CheckView()
    {
        if (GameManager.Instance.PlayerReference.GetInvisible()) return;
        bool SeePlayer= false;
        Transform HipPosition= GameManager.Instance.PlayerReference.HipsTransform();
        Transform HeadPosition= GameManager.Instance.PlayerReference.HeadTransform();
        Transform FeetPosition= GameManager.Instance.PlayerReference.transform;
        if (_rayCast.Checker<PlayerManager>(transform.position, (HipPosition.position - transform.position)) ||
           _rayCast.Checker<PlayerManager>(transform.position, (HeadPosition.position - transform.position)) ||
           _rayCast.Checker<PlayerManager>(transform.position, (FeetPosition.position - transform.position)))
            SeePlayer = true;
        _camera.SetTarget = SeePlayer;

    }
}
