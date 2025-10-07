using UnityEngine;

public class View_Move : Abstract_View
{
    public View_Move()
    {
    }
    public void Moving(bool State)
    {
        _animator.SetBool("isMoving", State);
    }
}
