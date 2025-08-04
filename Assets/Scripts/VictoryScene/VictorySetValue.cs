using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictorySetValue : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private bool _playOnce = false;
    private int _value = 0, _maxValue = 0;
    [SerializeField] private string _showText = "Money Stole : $";
    [SerializeField] private int _index;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        if (_index == 0)
        {
            _maxValue = UIManager.Instance.GetScore();
            _maxValue += 10000;
            GetComponentInParent<VictoryMenu>().SetMoney(_maxValue);
        }

        if (_index == 1)
        {
            _maxValue = GameManager.Instance.TimeCaptured();
            GetComponentInParent<VictoryMenu>().SetTries(_maxValue);
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
                AudioStorage.Instance.CountPoints();

            }

        }
    }
}
