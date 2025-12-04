using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_Crouch : Abstract_View
{
    private LayerMask _layerMask;
    private Transform _transform;
    public View_Crouch()
    {

    }
    public View_Crouch Layer(LayerMask Mask)
    {
        _layerMask = Mask;
        return this;
    }
    public View_Crouch Transform(Transform Transform)
    {
        _transform=Transform;
        return this;
    }
    public void Execute(bool NewState)
    {
        _animator.SetBool("isCrouching", NewState);
    }
}
