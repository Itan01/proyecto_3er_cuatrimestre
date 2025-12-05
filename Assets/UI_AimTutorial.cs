using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AimTutorial : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private bool _state;
    private void Start()
    {
        EventPlayer.Subscribe(EPlayer.aim, UITrigger);
        _animator = GetComponentInChildren<Animator>();
    }
    public void UITrigger(params object[] parameters)
    {
        _state = (bool)parameters[0];
        if (!_state)
        {
            EventPlayer.Unsubscribe(EPlayer.aim, UITrigger);
        }
        _animator.SetBool("Aiming", _state);
    }
    private void OnDestroy()
    {
        EventPlayer.Unsubscribe(EPlayer.aim, UITrigger);
    }
}
