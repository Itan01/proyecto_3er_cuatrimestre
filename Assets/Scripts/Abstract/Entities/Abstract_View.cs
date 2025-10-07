using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_View 
{
    protected Animator _animator;
    public Abstract_View()
    {
        _animator = null;
    }
    public Abstract_View Animator(Animator Animator)
    {
        _animator=Animator;
        return this;
    }
}
