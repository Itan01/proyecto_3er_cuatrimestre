using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Player
{   
    private Model_Move _move;
    private Model_Dash _dash;
    private Model_Crouch _crouch;
    private Model_Orientation _Orientation;
    public Model_Player(AbstractPlayer Player)
    {
        _Orientation = (Model_Orientation)new Model_Orientation().Weapon(Player.Weapon).ModelTransform(Player.ModelTransform());
        _move = (Model_Move)new Model_Move().Speed(6.0f).Transform(Player.transform).ModelTransform(Player.ModelTransform()).RB(Player.GetRb());
        _move = _move.Orientation(_Orientation);
        _dash= (Model_Dash)new Model_Dash().RB(Player.GetRb()).ModelTransform(Player.ModelTransform()).Transform(Player.transform);
        _dash = _dash.Force(10.0f);
        _crouch = (Model_Crouch)new Model_Crouch().Collider(Player.Collider()).Speed(3.5f).Transform(Player.transform);
        _crouch = _crouch.Move(_move).Speed(3.5f);

    }

    public void Gun()
    {
        _Orientation.Execute();
    }
    public void Move()
    {
        _move.Execute();
        _move.IsMoving = true;
    }
    public void DontMove()
    {
        _move.IsMoving = false; 
    }
    public void Dash()
    {
        _dash.Execute();
    }
    public void Crouch(bool NewState)
    {
        _crouch.Crouch(NewState);
    }
    public bool GetIsMoving()
    {
        return _move.IsMoving;
    }
}

