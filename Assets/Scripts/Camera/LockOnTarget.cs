using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LockOnTarget : MonoBehaviour
{
    private Transform _target;
    private Vector3 _view;

    private void Start()
    {
        _target= GameManager.Instance.CameraReference;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + _target.transform.forward, _target.transform.up);
    }
}
