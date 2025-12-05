using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour , ISoundInteractions
{
    private Animator _animator;
    [SerializeField]private SoundSummoner _sound;
    [SerializeField] private AudioClip _clip;
    private bool _playOnce=false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _sound = GetComponent<SoundSummoner>();
        _sound.enabled= false;
    }
    public void IIteraction(bool PlayerShootIt)
    {
        if (PlayerShootIt)
        {
            _animator.SetTrigger("destroy");
            if (!_playOnce)
            {
                _sound.enabled = true;
                AudioManager.Instance.PlaySFX(_clip,1.0f);
                _playOnce = true;
            }

        }
    }
}
