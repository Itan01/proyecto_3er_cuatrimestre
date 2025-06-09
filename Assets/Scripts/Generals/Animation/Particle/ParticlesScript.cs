using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesScript : MonoBehaviour
{
    private ParticleSystem _particles;
    void Start()
    {
        _particles = GetComponent<ParticleSystem>();
        GetComponentInParent<ParticlesSoundManager>().AddTolist(_particles);
    }
}
