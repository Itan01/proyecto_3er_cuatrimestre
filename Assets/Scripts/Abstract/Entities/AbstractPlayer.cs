using UnityEngine;

public class AbstractPlayer : EntityMonobehaviour, IEntity
{
    protected bool _canControl=false;
    protected bool _isDeath=false;
    [SerializeField] protected Abstract_Weapon _gun;
    [SerializeField]protected Transform _headTransform, _hipsTransform, _modelTransform;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
       base.Update();
    }
    public Animator GetAnimator()
    {
        return _animator;
    }

    public Rigidbody GetRb()
    {
        return _rb;
    }
    public Abstract_Weapon Gun
    {
        get { return _gun; }
        set { _gun = value; }
    }

    public Transform GetTransform()
    {
        return transform;
    }
    public Transform ModelTransform()
    {
        return _modelTransform;
    }

    public void SetSpeed(float Speed)
    {
        _speed = Speed;   
    }

    public void Control(bool State)
    {
        _canControl = State;
    }
    public bool IsDeath()
    {
        return _isDeath;
    }
    public Transform GetHipsPosition()
    {
        return _hipsTransform;
    }
    public Transform GetHeadPosition()
    {
        return _headTransform;
    }
}
