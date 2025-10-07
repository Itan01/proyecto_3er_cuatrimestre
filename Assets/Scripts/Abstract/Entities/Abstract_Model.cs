using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Abstract_Model
{
    protected Rigidbody _rb;
    protected Transform _modelTransform, _transform;
    public Abstract_Model()
    {
        _rb = null;
        _modelTransform = null;
        _transform = null;  
    }
    public Abstract_Model RB(Rigidbody Rb)
    {
        _rb = Rb;
        return this;
    }

    public Abstract_Model ModelTransform(Transform ModelTransform)
    {
        _modelTransform = ModelTransform;
        return this;
    }
    public Abstract_Model Transform(Transform Entity)
    {
        _transform = Entity;
        return this;
    }
    public abstract void Execute();

}
