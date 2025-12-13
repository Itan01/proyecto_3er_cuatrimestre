using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoorManagerExample : AbstracDoors, ISoundInteractions, ISoundAim
{
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private GameObject _uiExample;
    private float _soundVolume=1.0f;
    private ParticlesManager _particles;
    private Animation _animation;
    protected override void Start()
    {
       _animation=GetComponentInChildren<Animation>();
        _particles=GetComponentInChildren<ParticlesManager>();
       base.Start();
    }
    public override void CheckStatus()
    {
        if (_isDestroyed) return;
        _indexToDestroy--;
        for (int i = 0; i < _scriptText.Length; i++)
        {
            _scriptText[i].SetValue(_indexToDestroy, _maxValue);
        }
        // Debug.Log($" Remains {_indexToDestroy} to Open");
        if (_indexToDestroy <= 0)
            OpenDoor();

    }
    protected override void OpenDoor()
    {
        _animation.Play();
        base.OpenDoor();
    }
    public void IIteraction(bool PlayerShootIt)
    {
        if (PlayerShootIt)
        {
            _uiExample.SetActive(false);
            CheckStatus();
            _particles.PlayOnce();
            AudioSource.PlayClipAtPoint(_buttonSound, transform.position, _soundVolume);
        }
    }
}
