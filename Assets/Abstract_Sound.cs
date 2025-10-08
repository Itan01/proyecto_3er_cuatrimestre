using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Abstract_Sound: MonoBehaviour , ICanCatch
{
    [SerializeField] protected bool _atractted = false, _playerShooted=false, _canCatch=false;
    [SerializeField] protected ESounds _indexRef=ESounds.none;
    [SerializeField] protected float _speed = 1.0f, _size = 1.0f;
    protected Vector3 _velocity;
    protected Rigidbody _rb;
    protected Cons_LockOnTarget _lock;
    protected virtual void Start()
    {
        _rb=GetComponent<Rigidbody>();
        _lock = new Cons_LockOnTarget(transform);
    }

    protected virtual void Update()
    {
        
    }
    protected virtual void FixedUpdate()
    {
        transform.position += _speed * Time.fixedDeltaTime * _velocity.normalized;
    }
    protected virtual void LateUpdate()
    {
        _lock.Lock();
    }
    public virtual void ICatch()
    {
    }
    public void ForceDirection(Vector3 Orientation)
    {
        _velocity = Orientation;
    }

    public void Speed(float speed)
    {
        _speed = speed;
    }
    public void Size(float Size)
    {
        _size = Size;
    }
    public bool Atractted
    {
        get { return _atractted; }
        set { _atractted = value; }
    }
    public bool CanCatch
    {
        get { return _canCatch; }
        set { _canCatch = value; }
    }
    public ESounds IndexRef
    {
        get { return _indexRef; }
        set { _indexRef = value; }
    }


    public abstract void Refresh();
    private void OnTriggerEnter(Collider Entity)
    {
        if (Entity.TryGetComponent(out ISoundInteractions script))
        {
            script.IIteraction(_playerShooted);
        }
        //if (Entity.TryGetComponent(out PlayerManager player) && _canCatch)
        //{
        //   // Pasar el factory al que debe llamar
        //   //LVLManager.Instance.Gun.Bullet = this;
        //}
    }

}
