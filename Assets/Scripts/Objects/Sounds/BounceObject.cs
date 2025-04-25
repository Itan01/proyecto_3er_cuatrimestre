using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour
{
    [SerializeField] private float _distanceCheck = 1.0f;
    private float _timerBetweenBounce;
    private Ray _groundRay;
    [SerializeField] private LayerMask _groundRayMask;
    [SerializeField] private Vector3 _offSetPosition;
    [SerializeField] private bool _isGrounded;
    private Rigidbody _rb;
    [SerializeField] private float _forceGravity;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.mass = _forceGravity;
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = checkIfOnGround();
        if (_isGrounded)
        {
            MakeBounce();
        }
        _timerBetweenBounce += 1 * Time.deltaTime;
    }

    private void MakeBounce()
    {
        _timerBetweenBounce = Mathf.Clamp(_timerBetweenBounce, 0.1f, 10.0f);
        _rb.AddForce(transform.up * _timerBetweenBounce, ForceMode.Impulse);
        _timerBetweenBounce = 0.0f;
        _isGrounded = false;
    }

    private bool checkIfOnGround()
    {
        _offSetPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        _groundRay = new Ray(_offSetPosition, -transform.up);
        return Physics.Raycast(_groundRay, _distanceCheck, _groundRayMask);

    }
}
