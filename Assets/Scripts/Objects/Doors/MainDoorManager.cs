using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoorManager : MonoBehaviour
{
    [SerializeField] int _indexToDestroy;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        if (_indexToDestroy == 0)
        {
            _indexToDestroy = 1;
        }
    }

    public void CheckStatus()
    {
        _indexToDestroy--;
        Debug.Log($" Remains {_indexToDestroy} to Open");
        if (_indexToDestroy <= 0)
            OpenDoor();

    }
    private void OpenDoor()
    {
        Debug.Log(" OpenDoor");
        _animator.SetBool("isOpen",true);
    }
}
