using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    private PlayerManager _scriptManager;
    private ParticlesManager _particles;
    void Start()
    {
        _scriptManager = GetComponentInParent<PlayerManager>();
        _particles = GetComponentInChildren<ParticlesManager>();
    }
    public void ActivateAreaSound()
    {
        _scriptManager.SetAreaCatching(true);
    }

    public void DesactivateAreaSound()
    {
        _scriptManager.SetAreaCatching(false);
      
    }
    public void PlayerCanNotShoot()
    {
        _scriptManager.PlayerCanShoot(false);
    }
    public void PlayerCanShoot()
    {
        _scriptManager.PlayerCanShoot(true);
    }
    public void PlayWalkParticles()
    {
        _particles.PlayOnce();
    }

}