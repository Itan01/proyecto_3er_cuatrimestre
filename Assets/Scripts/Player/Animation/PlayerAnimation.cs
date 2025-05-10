using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation
{
    private Animator _animator;
    public PlayerAnimation(Animator Animator)
    {
        _animator = Animator;
    }
    public void SetBoolAnimator(string Text, bool State)
    {
        _animator.SetBool(Text, State);
    }
    public void SetFloatAnimator(string Text, float State)
    {
        _animator.SetFloat(Text, State);
    }
    public void SetTriggerAnimator(string Text)
    {
        _animator.SetTrigger(Text);
    }
    public void SetIntAnimator(string Text, int State)
    {
        _animator.SetInteger(Text, State);
    }

}
