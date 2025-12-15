using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MainDoorManager : AbstracDoors, ISoundInteractions
{
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private VisualEffect _effects;
    private float _soundVolume = 1.0f;
    protected Animator _animator;
    protected override void Start()
    {
        base.Start();
        _animator = GetComponentInChildren<Animator>();
    }
    protected override void OpenDoor()
    {
        _animator.SetBool("isOpen", true);
        base.OpenDoor();
        GetComponentInParent<RoomManager>().SetDoorDestroyed();
        AudioStorage.Instance.OpenDoorSound();
    }
    public void IIteraction(bool PlayerShootIt)
    {
        if (PlayerShootIt)
        {
            StartCoroutine(PlayParticles());
            CheckStatus();
            AudioSource.PlayClipAtPoint(_buttonSound, transform.position, _soundVolume);
        }
    }
    private IEnumerator PlayParticles()
    {
        _effects.SendEvent("OnPlayMainDoor");
        yield return new WaitForSeconds(1);
        _effects.SendEvent("OnStopMainDoor");
    }
}
