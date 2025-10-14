using UnityEngine;

public interface IEntity 
{
    public Animator GetAnimator();
    public abstract Rigidbody GetRb();
    public abstract Transform GetTransform();
    public abstract void SetSpeed(float Speed);
    public abstract void Control(bool State);
    public abstract Transform HipsTransform();
    public abstract Transform HeadTransform();

}
