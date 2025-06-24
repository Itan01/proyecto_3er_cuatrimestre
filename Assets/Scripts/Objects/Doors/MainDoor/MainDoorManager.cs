using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoorManager : AbstracDoors
{
    protected Animator _animator;
    protected override void Start()
    {
        base.Start();
        _animator = GetComponentInChildren<Animator>();
    }
    protected override void OpenDoor()
    {
        _animator.SetBool("isOpen", true);
        base.OpenDoor();
    }
}
