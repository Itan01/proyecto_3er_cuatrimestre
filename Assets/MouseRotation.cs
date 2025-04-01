using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseRotation : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _mousePostion;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _mousePostion = transform.position;
        _mousePostion.x += ((Input.mousePosition.x * 2 / Screen.width) - 1) * 1000;
        _mousePostion.z += ((Input.mousePosition.y * 2 / Screen.height) - 1) * 1000;
        _mousePostion.y = transform.position.y;
        
        Debug.Log(_mousePostion);
    }
    void FixedUpdate()
    {
        transform.LookAt(_mousePostion);
    }
}
