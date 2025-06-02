using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoorManager : MonoBehaviour
{
    [SerializeField] private int _maxValue;
    private int _indexToDestroy;
    private Animator _animator;
    private SetCountDoor _scriptText;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _scriptText = GetComponentInChildren<SetCountDoor>();
        if (_maxValue == 0)
        {
            _maxValue = 1;
        }
        _indexToDestroy = _maxValue;
        _scriptText.SetValue(_indexToDestroy, _maxValue);
    }

    public void CheckStatus()
    {
        _indexToDestroy--;
        _scriptText.SetValue(_indexToDestroy, _maxValue);
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
