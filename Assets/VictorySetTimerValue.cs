using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictorySetTimerValue : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private bool _playOnce = false;
    private int _value = 0, _maxValue = 0;
    private int _hour, _minute, _second;
    [SerializeField] private string _showText = "Time: ";
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();    
    }

    private void Update()
    {
        if (_value < _maxValue)
        {
            _value++;
            _second++;
            if (_second > 59)
            {
                _second = 0;
                _minute++;
            }
            if (_minute > 59)
            {
                _minute = 0;
                _hour++;
            }
            _text.text = $"{_showText} {_hour}:{_minute}:{_second}";
        }
        else
        {
            if (!_playOnce)
            {
                _playOnce = true;
                AudioStorage.Instance.CountPoints();
            }

        }
    }
}
