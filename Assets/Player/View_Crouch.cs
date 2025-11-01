using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_Crouch : Abstract_View
{
    private bool _isCrouching = false;
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
    public void Execute()
    {
        if (_isCrouching)
        {
            if (!Physics.BoxCast(_transform.position, new Vector3(0.6f, 0.5f, 0.6f), _transform.up, Quaternion.identity, 10.0f, _layerMask))
            {
                _isCrouching = false;
            }
        }
        else
            _isCrouching = true;
        _animator.SetBool("isCrouching", _isCrouching);
    }
}
