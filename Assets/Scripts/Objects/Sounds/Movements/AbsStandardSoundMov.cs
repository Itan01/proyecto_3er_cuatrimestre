using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class AbsStandardSoundMov : MonoBehaviour // Sonidos Genericos,Movimiento Base
{
    [SerializeField] protected float _speed = 0.0f, _refSpeed = 0.0f, _size = 0.0f, _rotSpeed = 0.0f;
    protected int _index = 0;
    [SerializeField] protected Vector3 _dir = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] protected Transform _target;
    [SerializeField] protected int _limit = 0;
    protected Rigidbody _rb;
    protected ShootingGun _scriptShoot;
    protected GrabbingGun _scriptGrab;

    protected virtual void Start(){
        BaseSettings();
    }

    protected virtual void Update()
    {
        if (_target)
            SetDirectionToTarget();
    }
    protected virtual void FixedUpdate(){
    }



    protected virtual void Move(){
        _rb.MovePosition(transform.position + _dir.normalized * _speed * Time.fixedDeltaTime);
    }

    protected virtual void SetDirectionToTarget()
    {
        _dir = (_target.position- transform.position).normalized;
        transform.forward = Vector3.Slerp(transform.forward, _dir.normalized, Time.fixedDeltaTime );
    }

    public void SetTarget(Transform Target, float Speed){
        _target = Target;
        if(Speed == 0.0f)
            _speed = _refSpeed;
        else
            _speed = Speed;
    }

    public virtual void Spawn(Vector3 Spawn, Vector3 Orientation,float Speed, float Size){
        transform.position = Spawn;
        _dir = Orientation - Spawn;
        _size = Size;
        _speed = Speed;
    }

    protected void BaseSettings()// En Caso de que no se especifique una Variable base
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX;
        _rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        if (_size == 0)
            _size = 1.0f;
        if (_speed == 0)
            _speed = _refSpeed;
        if (_rotSpeed == 0)
            _rotSpeed = 10.0f;
        if (_limit == 0)
            _limit = 1;
        if (_dir == new Vector3(0.0f,0.0f,0.0f))
            _dir.z = 1.0f;
    }

    protected void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.CompareTag("Player"))
        {
            _scriptShoot = Player.GetComponent<ShootingGun>();
            _scriptShoot.SetSound(_index, _speed, _size);
            _scriptShoot.CheckSound(true);
            _scriptGrab = Player.GetComponent<GrabbingGun>();
            _scriptGrab.CheckSound(true);
            Destroy(gameObject,0.1f);
        }
    }
}

