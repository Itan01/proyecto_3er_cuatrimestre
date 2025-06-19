using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionFade : MonoBehaviour
{
    private Animator _animator;
    private bool _state;
    void Start()
    {
        GameManager.Instance.Transition = this;
        _animator = GetComponent<Animator>();
        _state = true;
    }
    public void FadeOut()
    {
        _state= false;
        _animator.SetBool("State", _state);
    }
    public void ShowBlackScreen()
    {
        _state= true;
        _animator.SetBool("State", _state);
    }
}
