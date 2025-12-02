using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn_FadeOut : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        if(_animator == null) _animator = GetComponent<Animator>();
        EventManager.Subscribe(EEvents.Reset, ShowUp);
        EventManager.Subscribe(EEvents.ReStart, FadeOut);
    }
    private void ShowUp(params object[] Parameters)
    {
        _animator.SetBool("ShowUp", true);
    }
    private void FadeOut(params object[] Parameters)
    {
        _animator.SetBool("ShowUp", false);
    }
}
