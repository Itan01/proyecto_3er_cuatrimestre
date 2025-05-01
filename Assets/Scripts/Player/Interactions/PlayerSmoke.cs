using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSmoke : MonoBehaviour
{
    [SerializeField] private bool _isInSmoke = false, _cough = false;
    [SerializeField] GameObject _genericSound;
    private AbsStandardSoundMov _script;
    [SerializeField] private Image _smokeUI;
    [SerializeField] private Transform _model;
    private float _timer = 3.0f, _timerRef = 3.0f;
    private float _coughTimer = 1.0f, _coughTimerRef = 1.0f;

    private void Update()
    {
        if (_isInSmoke)
        {
            SubtractTimer();
        }
        else
        {
            _timer = _timerRef;
        }
        if (_cough)
        {
            TimerCough();
        }
    }
    public void ToggleState(bool State)
    {
        _isInSmoke = State;
        if (!_isInSmoke)
        {
            _cough = false;
            _smokeUI.color = new Color(255, 255, 255, 0);
        }
    }
    private void SubtractTimer()
    {
        if (_timer > 0)
        {
            _timer -= 1 * Time.deltaTime;
        }
        else if (_timer <= 0 && !_cough)
        {
            _timer = 0;
            _cough = true;
            
            _smokeUI.color = new Color(255,255,255,255); 
        }
    }
    private void TimerCough()
    {
        if (_coughTimer > 0)
        {
            _coughTimer -= 1 * Time.deltaTime;
        }
        else if (_coughTimer <= 0)
        {
            var Sound = Instantiate(_genericSound, transform.position + _model.transform.forward * 2, Quaternion.identity);
            _script = Sound.GetComponent<AbsStandardSoundMov>();
            _script.SetDirection(Sound.transform.position - _model.position, 5.0f, 1.0f);
            _coughTimer = _coughTimerRef;
            
        }
    }
}

