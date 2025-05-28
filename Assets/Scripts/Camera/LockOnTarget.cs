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
    void Update()
    {
        transform.LookAt(_target.position);
        transform.Rotate(0,180,0);
    }
}
