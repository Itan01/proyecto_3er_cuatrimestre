
using UnityEngine;

public class View_Dash : Abstract_View
{
    public View_Dash()
    {
    }
    public void Execute()
    {
        _animator.SetTrigger("Dash");
    }
}
