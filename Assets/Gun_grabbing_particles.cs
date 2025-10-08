using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_grabbing_particles : MonoBehaviour, IObserverMegaphone
{
    [SerializeField] private Particles_script[] _scripts;
    private void Awake()
    {
        GetComponentInParent<Gun>().AddObs(this);
    }

    public void Aiming()
    {
        throw new System.NotImplementedException();
    }
    public void StartShooting()
    {
        throw new System.NotImplementedException();
    }

    public void Grabbing(bool State)
    {
        foreach (var script in _scripts) 
        {
            script.PlayOnLoop(State);
        }
    }
}
