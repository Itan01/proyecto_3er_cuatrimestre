using System;
using UnityEngine;

public class PL_Control: IMVC
{
    private Control_Mov _movement;
    private Control_Interact _interact;
    private Control_Dash _dash;
    private Control_Crouch _crouch;
    private Control_Gun _gun;

    private event Action CheckInput, CheckFixedInput;
    public PL_Control(Model_Player Model, View_Player View)
    {
        _gun = (Control_Gun)new Control_Gun(this).View(View).Key(KeyCode.Mouse1);
        _gun=_gun.Gun(GameManager.Instance.PlayerReference.Weapon).ShootKey(KeyCode.Mouse0);
        _interact = (Control_Interact)new Control_Interact(this).View(View).Key(KeyCode.E);
        _interact = _interact.Layer(GameManager.Instance.PlayerReference.Layers()).SetEntity(GameManager.Instance.PlayerReference.GetHipsPosition());
        _movement = (Control_Mov)new Control_Mov(this).Model(Model).View(View);
        _dash = (Control_Dash)new Control_Dash(this).Model(Model).View(View).Key(KeyCode.LeftShift);
        _crouch = (Control_Crouch)new Control_Crouch(this).Model(Model).View(View).Key(KeyCode.LeftControl).AltKey(KeyCode.C);
        CheckInput += Model.Gun;
    }

    public void Execute()
    {
        CheckInput?.Invoke();
    }
    public void FixedExecute()
    {
        CheckFixedInput?.Invoke();
    }
    public void AddAction(Action Input)
    {
        CheckInput += Input;
    }
    public void RemoveAction(Action Input)
    {
        CheckInput -= Input;
    }
    public void FixedAddAction(Action Input)
    {
        CheckFixedInput += Input;
    }
    public void FixedRemoveAction(Action Input)
    {
        CheckFixedInput -= Input;
    }
}
