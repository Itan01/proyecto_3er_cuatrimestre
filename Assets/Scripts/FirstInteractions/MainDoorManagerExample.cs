using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoorManagerExample : AbstracDoors
{
    private Animation _animation;
    protected override void Start()
    {
       _animation=GetComponentInChildren<Animation>();
       base.Start();
    }
    public override void CheckStatus()
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
    protected override void OpenDoor()
    {
        _animation.Play();
        base.OpenDoor();
    }
}
