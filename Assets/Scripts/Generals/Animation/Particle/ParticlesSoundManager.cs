using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSoundManager : MonoBehaviour
{
    private AbstractSound _soundManager;
    [SerializeField]private List<ParticleSystem> _listParticles= new List<ParticleSystem>();
    private void Start()
    {
     _soundManager = GetComponentInParent<AbstractSound>();    
    }

    public void AddTolist(ParticleSystem particles)
    {
        _listParticles.Add(particles);
    }

    public void StarPlay()
    {
        foreach (ParticleSystem particle in _listParticles)
        {
            particle.Play();
        }
    }
}
