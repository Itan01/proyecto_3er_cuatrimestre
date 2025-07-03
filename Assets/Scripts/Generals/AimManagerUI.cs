using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimManagerUI : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]private GameObject _firstShootings;
    private int _uses = 0;
    private void Start()
    {
        UIManager.Instance.AimUI = this;
        _animator = GetComponent<Animator>();
    }

    public void UITrigger(bool State)
    {
        _animator.SetBool("Aiming", State);
        _uses++;
        if (_uses == 6)
        {
            _firstShootings.gameObject.SetActive(false);
        }
    }
}