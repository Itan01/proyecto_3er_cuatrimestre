using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Aim: MonoBehaviour
{
    private Animator _animator;
    [SerializeField]private bool _state;
    private void Start()
    {
        EventPlayer.Subscribe(EPlayer.aim, UITrigger);
        _animator = GetComponentInChildren<Animator>();
    }
    public void UITrigger(params object[] parameters)
    {
        _state = (bool)parameters[0];
        _animator.SetBool("Aiming", (bool)parameters[0]);
    }
}