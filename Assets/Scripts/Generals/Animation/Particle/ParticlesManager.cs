using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _listParticles = new List<ParticleSystem>();
    public void AddTolist(ParticleSystem particles)
    {
        _listParticles.Add(particles);
    }

    public void StartPlay()
    {
        foreach (ParticleSystem particle in _listParticles)
        {
            EmissionModule emission = particle.emission;
            emission.enabled= true;
        }
    }

    public void StopPlay()
    {
        foreach (ParticleSystem particle in _listParticles)
        {
            EmissionModule emission = particle.emission;
            emission.enabled = false;
        }
    }
}

