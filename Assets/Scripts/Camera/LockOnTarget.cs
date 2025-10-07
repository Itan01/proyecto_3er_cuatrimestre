using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    private Transform _target;

    private void Start()
    {
        _target= GameManager.Instance.CameraReference;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + _target.transform.forward, _target.transform.up);
    }
}
