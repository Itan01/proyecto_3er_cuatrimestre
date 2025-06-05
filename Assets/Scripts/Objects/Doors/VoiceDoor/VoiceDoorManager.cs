using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceDoorManager : AbstracDoors
{
    [SerializeField] private float _timer = 5.0f,_timerRef=5.0f;
    protected override void Start()
    {
        base.Start();
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
        _scriptText.gameObject.SetActive(true);
        _scriptText.SetValue(_indexToDestroy, _maxValue);
        _animator.speed = 1.0f;
    }
}