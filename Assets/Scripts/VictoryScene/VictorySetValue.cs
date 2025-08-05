using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictorySetValue : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private bool _playOnce = false;
    [SerializeField] private int _value = 0, _maxValue = 0;
    [SerializeField] private string _showText = "Money Stole : $";
    [SerializeField] private int _index;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    private VictoryMenu _menu;

    private void Start()
    {
        _menu= GetComponentInParent<VictoryMenu>();
        _text = GetComponent<TextMeshProUGUI>();
        if (_index == 0)
        {
            _maxValue = GameManager.Instance.ScoreValue;
            _menu.SetMoney(_maxValue);
        }

        if (_index == 1)
        {
            _maxValue = GameManager.Instance.TimeCaptured();
            _menu.SetTries(_maxValue);
        }
    }
    private void Update()
    {
        if (_value < _maxValue)
        {
            _value++;
            _text.text = $"{_showText} {_value}";

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
