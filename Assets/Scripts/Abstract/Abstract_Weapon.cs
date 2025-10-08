using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Weapon : MonoBehaviour, IObservableMegaphone
{
    [SerializeField]protected bool _useLeftClick=false;
    [SerializeField] protected bool _useRightClick=false;
    [SerializeField] protected bool _hasBullet;
    [SerializeField] protected List<IObserverMegaphone> _obs = new List<IObserverMegaphone>();

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

    public void AddObs(IObserverMegaphone Obj)
    {
        if (!_obs.Contains(Obj))
        {
            _obs.Add(Obj);
        }
    }
    public void RemoveObs(IObserverMegaphone Obj)
    {
        if (_obs.Contains(Obj))
        {
            _obs.Remove(Obj);
        }
    }
}
