using UnityEngine;

public class View_Player
{
    private View_Move _move;
    private View_Dash _dash;
    private View_Crouch _crouch;
    private View_Gun _gun;
    private View_Interact _interact;
    public View_Player(AbstractPlayer Player)
    {
        _move = (View_Move)new View_Move().Animator(Player.GetAnimator());
        _interact = (View_Interact)new View_Interact().Animator(Player.GetAnimator());
        _dash = (View_Dash)new View_Dash().Animator(Player.GetAnimator());
        _crouch= (View_Crouch)new View_Crouch().Animator(Player.GetAnimator());
        _crouch= _crouch.Transform(Player.transform).Layer(Player.Layers()._everything);
        _gun= (View_Gun)new View_Gun().Animator(Player.GetAnimator());
    }
    public void Interact()
    {
        _interact.Execute();
    }
    public void Move()
    {
        _move.Moving(true);
    }
    public void DontMove()
    {
        _move.Moving(false);
    }
    public void Dash()
    {
        _dash.Execute();
    }
    public void Crouch()
    {
        _crouch.Execute();
    }
    public void GunPrimary(bool State)
    {
        _gun.PrimaryFire(State);
    }
    public void GunSecondary(bool State)
    {
        _gun.SecondaryFire(State);
    }
}
