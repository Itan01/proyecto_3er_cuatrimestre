using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMov : MonoBehaviour
{
    [SerializeField]private float _size,_speed;
    private Vector3 _dir, _forceMove;
    private GameObject _enemyTarget, _playerTarget;
    public Vector3 _startPoint;
    private grab_and_shoot _scriptPlayer;

    private Rigidbody _rb;
    void Start()
    {
        _startPoint = transform.position;
        Destroy(gameObject, 10);
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }
    void FixedUpdate()
    {
        if (_playerTarget != null)
        {
            MoveToPlayer();
            _rb.MovePosition(transform.position + _forceMove * _speed * 1.5f * Time.fixedDeltaTime);
        }
        else if (_enemyTarget != null)
        {
            MoveToEnemy();
            _rb.MovePosition(transform.position + _forceMove * _speed * Time.fixedDeltaTime);
        }
        else 
        {
        _rb.MovePosition(transform.position + _dir * _speed * Time.fixedDeltaTime);
         }
    }
    public void SetVector(Vector3 Orietation,float Size, string TypeOfSound)
    {
        _dir =(Orietation - transform.position).normalized;
        _size = Size;
        transform.localScale = new Vector3(_size, _size, _size);
        _speed =Size*5;
    }

    public void NewEnemyTarget(GameObject target)
    {
        _enemyTarget = target;
    }
    public void PlayerTarget(GameObject target)
    {
        _playerTarget = target;
    }

    private void MoveToPlayer()
    {
        _forceMove= (_playerTarget.transform.position- transform.position).normalized;
    }
    private void MoveToEnemy()
    {
        _forceMove = (_enemyTarget.transform.position-transform.position).normalized;
    }

    void OnTriggerEnter(Collider player)
    {

        if (player.gameObject.CompareTag("Player"))
        {
            _scriptPlayer = player.GetComponent<grab_and_shoot>();

            if (_scriptPlayer._catchingSound)
            {
                _scriptPlayer.CatchSound(_size);
                Destroy(gameObject);
            }
        }
    }
}
