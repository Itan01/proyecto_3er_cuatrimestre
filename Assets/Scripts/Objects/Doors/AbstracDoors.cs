using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstracDoors : MonoBehaviour
{
    [SerializeField] protected int _maxValue;
    protected int _indexToDestroy;
    protected Animator _animator;
    [SerializeField] protected SetCountDoor[] _scriptText;
    protected bool _isDestroyed;
    protected virtual void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        if (_maxValue == 0)
        {
            _maxValue = 1;
        }
        _indexToDestroy = _maxValue;
        for (int i = 0; i < _scriptText.Length; i++)
        {
            _scriptText[i].SetValue(_indexToDestroy, _maxValue);
        }
        
    }
    protected virtual void Update()
    { }
    public virtual void CheckStatus()
    {
        if (_isDestroyed) return;
        _indexToDestroy--;
        for (int i = 0; i < _scriptText.Length; i++)
        {
            _scriptText[i].SetValue(_indexToDestroy, _maxValue);
        }
       // Debug.Log($" Remains {_indexToDestroy} to Open");
        if (_indexToDestroy <= 0)
            OpenDoor();

    }
    protected void OpenDoor()
    {
        _animator.SetBool("isOpen", true);
        for (int i = 0; i < _scriptText.Length; i++)
        {
            _scriptText[i].gameObject.SetActive(false);
        }

        _isDestroyed = true;
    }
}
