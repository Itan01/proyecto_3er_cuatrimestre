using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectAnimation : MonoBehaviour
{
    private Animator _animator;
    private ControlAnimator _controlAnimator;
    private bool _isPlaying;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _controlAnimator = new ControlAnimator(_animator);
    }

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.TryGetComponent<PlayerManager>(out PlayerManager script))
        {
            _isPlaying=true;
            if(!_isPlaying)
            _controlAnimator.SetBoolAnimator("Shine", _isPlaying);
        }
    }

    private void OnTriggerExit(Collider Player)
    {
        if (Player.TryGetComponent<PlayerManager>(out PlayerManager script))
        {
            _isPlaying = false;
            _controlAnimator.SetBoolAnimator("Shine", _isPlaying);
        }
    }
}
