using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Particles_script : MonoBehaviour
{
    private ParticleSystem _particles;
    private void Start()
    {
        _particles = GetComponent<ParticleSystem>();
    }
    public void PlayOneTime()
    {
        _particles.Play();
    }
    public void PlayOnLoop(bool State)
    {
        var x = _particles.main;
        x.loop = State;
        if (State)
            _particles.Play();
        else
            _particles.Stop();
    }

}
