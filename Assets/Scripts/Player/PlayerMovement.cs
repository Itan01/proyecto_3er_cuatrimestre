using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _movSpeed = 3.5f;
    private Rigidbody _rb;
    private Vector3 _dir;
    private bool _isMoving = false;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _isMoving=CheckIfMoving();
        
    }
    void FixedUpdate()
    {
        if (_isMoving)
        {
            _rb.MovePosition(transform.position + _dir.normalized * _movSpeed * Time.fixedDeltaTime);
        }
    }
    
    private bool CheckIfMoving()
    {
        _dir.x = Input.GetAxisRaw("Horizontal");
        _dir.z = Input.GetAxisRaw("Vertical");
        if(_dir.sqrMagnitude != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
