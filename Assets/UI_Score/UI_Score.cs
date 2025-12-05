using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Score : MonoBehaviour, IObservableScore
{
    private List<IObserverScore> _obs= new List<IObserverScore>();
    private float _value=0.0f;

    private void Start()
    {
        EventPlayer.Subscribe(EPlayer.steal,UpdateValue);
    }

    public void UpdateValue(params object[] Parameters)
    {
        if (_obs == null) return;
        foreach (var Obs in _obs)
        {
            Obs.Execute(_value, (float)Parameters[0]);
        }
        _value += (float)Parameters[0];
    }
    public void AddObs(IObserverScore Obj)
    {
        if (!_obs.Contains(Obj))
        {
            _obs.Add(Obj);
        }
    }

    public void RemoveObs(IObserverScore Obj)
    {
        if (_obs.Contains(Obj))
        {
            _obs.Remove(Obj);
        }
    }
    public int GetScore()
    {
        return (int)_value;
    }
}
