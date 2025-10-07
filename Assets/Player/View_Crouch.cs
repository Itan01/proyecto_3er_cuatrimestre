using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_Crouch : Abstract_View
{
    private bool _isCrouching = false;
    public View_Crouch()
    {

    }
    public void Execute()
    {
        if (_isCrouching)
        {
            _isCrouching = false;
        }
        else
            _isCrouching = true;
        _animator.SetBool("isCrouching", _isCrouching);
    }
}
