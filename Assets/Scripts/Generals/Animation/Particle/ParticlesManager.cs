using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _listParticles = new List<ParticleSystem>();
    [SerializeField] private bool _animating=false;
    private void Start()
    {
    }
    public void AddTolist(ParticleSystem particles)
    {
        _listParticles.Add(particles);
    }
    public bool IsAnimating()
    {
        return _animating;
    }
    public void StartLoop()
    {
        foreach (ParticleSystem particle in _listParticles)
        {
            EmissionModule emission = particle.emission;
            emission.enabled= true;

        }
        _animating = true;
    }

    public void StopLoop()
    {
        foreach (ParticleSystem particle in _listParticles)
        {
            EmissionModule emission = particle.emission;
            emission.enabled = false;
        }
        _animating = true;
    }
    public void PlayOnce()
    {
        foreach (ParticleSystem particle in _listParticles)
        {
            particle.Play();
        }
    }
}

