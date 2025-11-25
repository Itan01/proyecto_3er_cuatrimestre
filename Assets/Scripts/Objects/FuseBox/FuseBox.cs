using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour , ISoundInteractions
{
    private Animator _animator;
    [SerializeField]private SoundSummoner _SOUND;   
    private bool _playOnce=false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _SOUND = GetComponent<SoundSummoner>();
    }
    public void IIteraction(bool PlayerShootIt)
    {
        if (PlayerShootIt)
        {
            _animator.SetTrigger("destroy");
            if (!_playOnce)
            {
                _SOUND.enabled = true;
                //AudioStorage.Instance.RoombaExplosion();
                AudioStorage.Instance.SoundsGameObject(EAudios.FuseBox);
                _playOnce = true;
            }

        }
    }
}
