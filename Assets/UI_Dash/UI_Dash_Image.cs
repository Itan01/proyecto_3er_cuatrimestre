using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Dash_Image : MonoBehaviour, IObserverDash
{
    Action VirtualUpdate;
    private Image _image;
    private float _timer, _maxTimer;
    private void Start()
    {
        GetComponentInParent<IObservableDash>().AddObs(this);
        _image=GetComponent<Image>();
    }
    private void Update()
    {
        VirtualUpdate?.Invoke();


    }
    public void Execute(float Time)
    {
        _maxTimer= Time;
        _timer =0.0f;
        VirtualUpdate = Timer;
    }
    private void Timer()
    {
        if (_timer > _maxTimer)
        {
            _timer = _maxTimer;
            VirtualUpdate = null;
        }
        _timer += Time.deltaTime;
        _image.fillAmount = _timer / _maxTimer;
    }
}
