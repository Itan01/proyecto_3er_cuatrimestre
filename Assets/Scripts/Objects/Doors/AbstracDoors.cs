using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstracDoors : MonoBehaviour
{
    [SerializeField] protected int _maxValue;
    protected int _indexToDestroy;
    protected Animator _animator;
    [SerializeField] protected SetCountDoor _scriptText;
    protected bool _isDestroyed;
    protected virtual void Start()
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
    protected virtual void Update()
    { }
    public virtual void CheckStatus()
    {
        if (_isDestroyed) return;
        _indexToDestroy--;
        _scriptText.SetValue(_indexToDestroy, _maxValue);
        Debug.Log($" Remains {_indexToDestroy} to Open");
        if (_indexToDestroy <= 0)
            OpenDoor();

    }
    protected void OpenDoor()
    {
        _animator.SetBool("isOpen", true);
        _scriptText.gameObject.SetActive(false);
        _isDestroyed = true;
    }
}
