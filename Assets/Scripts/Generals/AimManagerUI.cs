using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimManagerUI : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        GameManager.Instance.AimUI = this;
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void UITrigger(bool State)
    {
        if (State)
            _animator.SetTrigger("Start");
        else
            _animator.SetTrigger("Stop");
    }
}
