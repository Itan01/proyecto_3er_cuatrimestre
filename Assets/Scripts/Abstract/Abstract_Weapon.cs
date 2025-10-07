using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Weapon : MonoBehaviour
{
    [SerializeField]protected bool _useLeftClick=false;
    [SerializeField] protected bool _useRightClick=false;
    [SerializeField] protected bool _hasBullet;

    public bool UseRightClick
    {
        get { return _useRightClick; }
        set { _useRightClick = value; }
    }
    public bool UseLeftClick
    {
        get { return _useLeftClick; }
        set { _useLeftClick = value; }
    }
}
