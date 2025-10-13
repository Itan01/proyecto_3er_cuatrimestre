using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Dash : MonoBehaviour, IObservableDash
{
    private List<IObserverDash> _obs = new List<IObserverDash>();
    private void Start()
    {
        EventPlayer.Subscribe(EPlayer.dash, Subscription);
    }
    private void Subscription(params object[] Parameters)
    {
        float Timer = (float)Parameters[0];
        foreach (var Obs in _obs)
        {
            Obs.Execute(Timer);
        }
    }
    public void AddObs(IObserverDash Obj)
    {
        if (!_obs.Contains(Obj))
        {
            _obs.Add(Obj);
        }
    }

    public void RemoveObs(IObserverDash Obj)
    {
        if (_obs.Contains(Obj))
        {
            _obs.Remove(Obj);
        }
    }
}
