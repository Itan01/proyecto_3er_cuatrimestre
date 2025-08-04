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
    private string _textSec, _textMin, _textHour;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _maxValue=Mathf.FloorToInt( UIManager.Instance.FinalTime);
        GetComponentInParent<VictoryMenu>().SetTime(_maxValue);
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
                if (_second < 10)
                    _textSec = $"0{_second}";
                if (_minute < 10)
                    _textMin = $"0{_minute}";
                if (_hour < 10)
                    _textHour = $"0{_hour}";
                _text.text = $"{_showText} {_textHour}:{_textMin}:{_textSec}";
                _playOnce = true;
                AudioStorage.Instance.CountPoints();

            }
        }
    }
}
