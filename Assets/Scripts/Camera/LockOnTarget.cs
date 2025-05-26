using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    private CameraManager _target;

    private void Start()
    {
        _target= FindAnyObjectByType<CameraManager>();
    }
    void Update()
    {
        transform.LookAt(_target.transform.position); 
    }
}
