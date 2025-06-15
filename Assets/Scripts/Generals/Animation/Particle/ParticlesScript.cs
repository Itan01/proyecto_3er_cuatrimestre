using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesScript : MonoBehaviour
{
    private ParticleSystem _particles;
    private void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
        GetComponentInParent<ParticlesManager>().AddTolist(_particles);
    }
}
