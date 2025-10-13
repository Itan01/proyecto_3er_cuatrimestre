
using UnityEngine;

public class View_Dash : Abstract_View
{
    public View_Dash()
    {
    }
    public void Execute()
    {
        EventPlayer.Trigger(EPlayer.dash,3.0f);
        _animator.SetTrigger("Dash");
    }
}
