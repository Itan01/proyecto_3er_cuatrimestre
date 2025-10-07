using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_Gun : Abstract_View
{
    public View_Gun()
    {

    }
    public void PrimaryFire(bool State)
    {
        _animator.SetBool("Grabbing", State);
    }
    public void SecondaryFire(bool State)
    {
        _animator.SetBool("StartShooting", State);
    }
}
