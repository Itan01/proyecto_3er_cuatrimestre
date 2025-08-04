using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictorySetRank : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private void Start()
    {
        _text=GetComponent<TextMeshProUGUI>();
    }
    public void SetRank(int Value)
    {
        if (Value > 1000)
        {
            _text.text = "S";
        }
        else if (Value > 500)
        {
            _text.text = "A";
        }
        else if (Value > 250)
        {
            _text.text = "B";
        }
        else if (Value > 100)
        {
            _text.text = "C";
        }
        else
        {
            _text.text = "D";
        }
    }
}
