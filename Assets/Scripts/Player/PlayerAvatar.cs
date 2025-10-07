using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    private PlayerManager _scriptManager;
    private ParticlesManager _particles;
    private AudioClip _audio_walk;
    private AudioSource _audio;
    private Rigidbody _rb;
    void Start()
    {
        _rb=GetComponentInParent<Rigidbody>();
        _audio =GetComponentInParent<AudioSource>();
        _audio_walk = AudioStorage.Instance.PlayerSound(EAudios.PlayerWalk);
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
        //_scriptManager.PlayerCanShoot(false);
    }
    public void PlayerCanShoot()
    {
       // _scriptManager.PlayerCanShoot(true);
    }
    public void PlayWalkParticles()
    {
        _particles.PlayOnce();
        PlayWalkAudio();
    }
    private void PlayWalkAudio()
    {
        _audio.pitch= Random.Range(0.9f,2.1f);
        _audio.PlayOneShot(_audio_walk,0.1f);
    }
    public void SetGravityTrue()
    {
        _rb.useGravity = true;
    }
    public void SetGravityFalse()
    {
        _rb.useGravity = false;
    }
}