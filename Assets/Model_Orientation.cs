using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Orientation : Abstract_Model
{
    private Vector3 _orientation;
    private Abstract_Weapon _weapon;
    public Model_Orientation()
    {
        _orientation= Camera.main.transform.forward;
        _weapon= null;
    }
    public Model_Orientation Weapon( Abstract_Weapon Weapon)
    {
        _weapon = Weapon;
        return this;
    }
    public void Set(Vector3 NewOrientation)
    {
        _orientation=NewOrientation;
    }
    public override void Execute()
    {
        if (_weapon.IsUsing())
        {
            _orientation = Camera.main.transform.forward;
            _orientation.y = 0.0f;
        }
        _modelTransform.forward = _orientation;
    }
}
