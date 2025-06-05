using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoorManagerExample : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void CheckStatus()
    {
        Debug.Log(" OpenDoor");
        _animator.SetBool("isOpen", true);
    }
}
