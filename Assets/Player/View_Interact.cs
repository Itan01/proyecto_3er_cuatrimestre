using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_Interact : Abstract_View
{
    public View_Interact()
    {

    }
    public void Execute()
    { 
        _animator.SetTrigger("Steal");
    }
}
