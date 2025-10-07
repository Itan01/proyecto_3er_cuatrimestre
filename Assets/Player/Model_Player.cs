using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Player
{   
    private Model_Move _move;
    private Model_Dash _dash;
    private Model_Gun gun;
    private Model_Crouch _crouch;
    public Model_Player(AbstractPlayer Player)
    {
        _move= (Model_Move)new Model_Move().Speed(6.0f).Transform(Player.transform).ModelTransform(Player.ModelTransform()).RB(Player.GetRb());
        _dash= (Model_Dash)new Model_Dash().RB(Player.GetRb()).ModelTransform(Player.ModelTransform()).Transform(Player.transform);
        _crouch = new Model_Crouch(_move);
        gun = new Model_Gun(Player.Gun);
    }

    public void Move()
    {
        _move.Execute();
    }
    public void Dash()
    {
        _dash.Execute();
    }
    public void Crouch()
    {
        _crouch.Execute();
    }
}

