using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Gun : Abstract_Control
{
    private KeyCode _shootKey;
    private Abstract_Weapon _gun;
    public Control_Gun(PL_Control Controller)
    {
        Controller.AddAction(Execute);
        _gun = null;
        _shootKey = KeyCode.None;
    }
    public Control_Gun ShootKey(KeyCode Key)
    {
        _shootKey= Key;
        return this;
    }
    public Control_Gun Gun(Abstract_Weapon Weapon)
    {
        _gun = Weapon;
        return this;
    }
    public override void Execute()
    {
        if (Input.GetKey(_key))
        {
            _view.GunPrimary(true);   
            _gun.UseRightClick = true;
        }
        else
        {
            _view.GunPrimary(false);
            _gun.UseRightClick = false;
        }
        if (Input.GetKey(_shootKey))
        {
            _view.GunSecondary(true);
            _gun.UseLeftClick = true;
        }
        else
        {
            _view.GunSecondary(false); 
            _gun.UseLeftClick = false;
        }
    }
}
