using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cons_LockOnTarget
{
    private Transform _target;
    private Transform _transform;
    public Cons_LockOnTarget(Transform Transform)
    {
        _transform= Transform;
        _target = GameManager.Instance.CameraReference;
    }
    public void Lock()
    {
        _transform.LookAt(_transform.position + _target.transform.forward, _target.transform.up);
    }
}
