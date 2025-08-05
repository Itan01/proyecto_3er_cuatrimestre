using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictorySetTimerValue : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private bool _playOnce = false;
    [SerializeField]private int _value = 0, _maxValue = 0;
    private int _hour, _minute, _second;
    [SerializeField] private string _showText = "Time: ";
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private VictoryMenu _menu;
    private string _textSec, _textMin, _textHour;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _maxValue=Mathf.FloorToInt(GameManager.Instance.TimeOnlevel);
        _menu.SetTime(_maxValue);
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
            _text.text = $"{_showText} {_hour:00}:{_minute:00}:{_second:00}";
        }
        else
        {
            if (!_playOnce)
            {
                _playOnce = true;
                _audioSource.PlayOneShot(_audioClip);

            }
        }
    }
}
