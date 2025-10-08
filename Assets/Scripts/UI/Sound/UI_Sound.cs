using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class UI_Sound : MonoBehaviour, IObserverMegaphone
{
    private Image _uI;
    [SerializeField]private Sprite[] _sounds;
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _uI = GetComponentInChildren<Image>();
        SetSound(ESounds.none);
        LVLManager.Instance.Gun.AddObs(this);
    }
    public void SetSound(ESounds Sound) 
    {   if (Sound == ESounds.none)
        {
            ResetSound();
        }
        int a = (int)Sound;
        _animator.SetTrigger("Grab_Sound");
        _uI.sprite = _sounds[a];
    }
    public void ResetSound()
    {
        _animator.SetTrigger("Shoot_Sound");
        _uI.sprite = _sounds[0];
    }
}
