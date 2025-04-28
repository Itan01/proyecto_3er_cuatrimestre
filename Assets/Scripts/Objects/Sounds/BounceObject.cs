using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour
{
    [SerializeField] private float _forceGravity;
    private float _distanceCheck = 0.7f;
    private Ray _groundRay;
    [SerializeField] private LayerMask _groundRayMask;
    private Vector3 _offSetPosition;
    private Rigidbody _rb;

     void Start()
     {
         _rb = GetComponent<Rigidbody>();
         _rb.mass = _forceGravity;
     }

    public void MakeBounce(float Time)
    {
        Time = Mathf.Clamp(Time, 0.3f, 10.0f);
        _rb.AddForce(transform.up * Time* _forceGravity*8, ForceMode.Impulse);
    }

    public bool CheckIfOnGround()
    {
        _offSetPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        _groundRay = new Ray(_offSetPosition, -transform.up);
        return Physics.Raycast(_groundRay, _distanceCheck, _groundRayMask);
    }
}
