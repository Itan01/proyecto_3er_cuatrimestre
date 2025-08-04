using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictorySetRank : MonoBehaviour
{
    private int _value;
    private TextMeshProUGUI _text;
    private void Start()
    {
        _text=GetComponent<TextMeshProUGUI>();
    }
    public void SetRank(int Value)
    {
        _value=Value;
        if (_value > 1000)
        {
            _text.text = $"S";
        }
        if (_value > 500)
        {
            _text.text = $"A";
        }
        if (_value > 250)
        {
            _text.text = $"B";
        }
        if (_value > 50)
        {
            _text.text = $"C";
        }
        if (_value <= 50)
        {
            _text.text = $"D";
        }
    }
}
