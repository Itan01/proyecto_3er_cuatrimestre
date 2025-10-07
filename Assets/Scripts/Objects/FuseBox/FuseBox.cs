using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour , ISoundInteractions
{
    private Animator _animator;
    private bool _playOnce=false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void IIteraction(bool PlayerShootIt)
    {
        if (PlayerShootIt)
        {
            _animator.SetTrigger("destroy");
            if (!_playOnce)
            {
                //AudioStorage.Instance.RoombaExplosion();
                AudioStorage.Instance.SoundsGameObject(EAudios.FuseBox);
                _playOnce = true;
            }

        }
    }
}
