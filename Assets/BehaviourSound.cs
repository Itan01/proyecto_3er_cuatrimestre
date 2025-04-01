using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourSound : MonoBehaviour
{
    private Rigidbody _rb;
    private float _speed;
    private Vector3 _dir;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.MovePosition(transform.position + _dir.normalized * _speed * Time.fixedDeltaTime);
    }

    public void SetMotion(float Modification)
    {
        _speed = Modification;
    }
}
