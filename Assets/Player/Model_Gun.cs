using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Gun : Abstract_Model
{
    private Abstract_Weapon _weapon;
    public Model_Gun(Abstract_Weapon Gun)
    {
        _weapon = Gun;
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }
}
