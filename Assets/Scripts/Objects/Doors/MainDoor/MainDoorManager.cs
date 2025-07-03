using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoorManager : AbstracDoors, ISoundInteractions
{
    [SerializeField] private AudioClip _buttonSound;
    private ParticlesManager _particles;
    private float _soundVolume = 1.0f;
    protected Animator _animator;
    protected override void Start()
    {
        base.Start();
        _animator = GetComponentInChildren<Animator>();
        _particles=GetComponentInChildren<ParticlesManager>();
    }
    protected override void OpenDoor()
    {
        _animator.SetBool("isOpen", true);
        base.OpenDoor();
        AudioStorage.Instance.OpenDoorSound();
    }
    public void IIteraction(bool PlayerShootIt)
    {
        if (PlayerShootIt)
        {
            _particles.PlayOnce();
            CheckStatus();
            AudioSource.PlayClipAtPoint(_buttonSound, transform.position, _soundVolume);
        }
    }
}
