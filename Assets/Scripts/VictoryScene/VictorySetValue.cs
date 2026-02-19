using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictorySetValue : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private int _value = 0, _maxValue = 0;
    [SerializeField] private string _showText = "Money Stole : $";
    [SerializeField] private int _index;
    [SerializeField] private AudioClip _audioClip;
    private VictoryMenu _menu;

    private void Start()
    {
        _menu= GetComponentInParent<VictoryMenu>();
        _text = GetComponent<TextMeshProUGUI>();
        if (_index == 0)
        {
            _maxValue = GameManager.Instance.FinalScore;
            _menu.SetMoney(_maxValue);
            Save_Progress_Json.Instance.Data.Money += _maxValue;
            Save_Progress_Json.Instance.SaveData();
        }

        if (_index == 1)
        {
            _maxValue = GameManager.Instance.TimesCaptured;
            _menu.SetTries(_maxValue);
        }
        StartCoroutine(UpdateValue());
    }
    private IEnumerator UpdateValue()
    {
        while (_value < _maxValue)
        {
            _value += (int)(Time.deltaTime * _maxValue * 0.5f);
            _text.text = $"{_showText} {_value}";

        }
        AudioManager.Instance.PlaySFX(_audioClip,1.0f);
        yield return null;
    }
}
