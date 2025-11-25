using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Gun_particles_Grabbing : MonoBehaviour,IObserverMegaphone
{
   [SerializeField] private VisualEffect _effect;
    [SerializeField] private bool _isGrabbing=false;
    private void Start()
    {
        LVLManager.Instance.Gun.AddObs(this);
        _effect = GetComponent<VisualEffect>();
        _effect.Stop();
    }
    public void Grabbing(bool State)
    {
        if(!_isGrabbing && State)
        {
            _isGrabbing=true;
            _effect.SendEvent("OnGrabPlay");
            _effect.Play();     
        }
        if (!State == _isGrabbing)
        {
            _effect.SendEvent("OnGrabStop");
            _isGrabbing = false;

        }
    }
    public  void Aiming() { }
}
