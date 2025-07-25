using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BounceObject
{
    private float _distanceCheck = 0.7f;
    private Ray _groundRay;
    private LayerMask _groundRayMask;
    private Rigidbody _rb;
    private Transform _transform;

    public BounceObject(LayerMask LayerGround, Transform TransformObject, Rigidbody rb)
    {
        _groundRayMask = LayerGround;
        _transform = TransformObject;
        _rb = rb;
    }

    public void MakeBounce(float Time, float Power)
    {
        Time = Mathf.Clamp(Time, 1.0f, 5.0f);
        _rb.AddForce(_transform.up * Time* Power, ForceMode.Impulse);
    }

    public bool CheckIfOnGround(float Size)
    {
        Vector3 _offSetPosition = _transform.position + new Vector3(0.0f, (Size / 2), 0.0f);
        _distanceCheck = (Size / 2) + 0.1f;
        _groundRay = new Ray(_offSetPosition, -_transform.up);
        return Physics.Raycast(_groundRay, _distanceCheck, _groundRayMask);
    }
}
