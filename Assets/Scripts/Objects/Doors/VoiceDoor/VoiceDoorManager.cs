using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceDoorManager : AbstracDoors
{
    [SerializeField] private float _timer = 5.0f,_timerRef=5.0f;
    private Animator _animator; 
    protected override void Start()
    {
        base.Start();
        _animator= GetComponent<Animator>();    
        _timerRef = _timer;
    }
    protected override void Update()
    {
        if (_isDestroyed)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                CloseDoors();
            }
        }
    }
    private void CloseDoors()
    {
        _isDestroyed = false;
        _timer = _timerRef;
        _animator.SetBool("isOpen", false);
        _indexToDestroy = _maxValue;
        for (int i = 0; i < _scriptText.Length; i++)
        {
            _scriptText[i].gameObject.SetActive(true);
            _scriptText[i].SetValue(_indexToDestroy, _maxValue);
        }
       
        _animator.speed = 1.0f;
    }
}