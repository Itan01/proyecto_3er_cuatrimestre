using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementTypeTwo : MonoBehaviour
{
    [SerializeField] private bool _enable = false;
    [SerializeField] private float _movSpeed=3.5f;
    private Vector3 _dir;
    private Vector3 _moveToPosition;
    private Rigidbody _rb;
    private EnemySettings _manager;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _manager = GetComponent<EnemySettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_enable)
        {
            GetDirection();

        } 
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + _dir.normalized * _movSpeed * Time.fixedDeltaTime);
    }

    public void SetActivate(bool setting)
    {
        _enable = setting;
    }
    public void SetPostionToFollow(Vector3 _position)
    {
        _moveToPosition = _position;
    }
    private void GetDirection()
    {
        transform.LookAt(_moveToPosition);
        _dir = _moveToPosition - transform.position;
        if (_dir.magnitude <= 0.3)
        {
            _manager.SetTypeOfMovement(1);
        }


    }
}
