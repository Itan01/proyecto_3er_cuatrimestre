using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictorySetRank : MonoBehaviour
{
    private int _value;
    private TextMeshProUGUI _text;
    [SerializeField] private AudioClip[] _ranks;
    private void Start()
    {
        _text=GetComponent<TextMeshProUGUI>();
    }
    public void SetRank(int Value)
    {
        _value=Value;
        if (_value <= 50)
        {
            _text.text = $"D";
            AudioManager.Instance.PlaySFX(_ranks[0],7.0f);
        }
        if (_value > 50)
        {
            _text.text = $"C";
            AudioManager.Instance.PlaySFX(_ranks[1], 7.0f);
        }
        if (_value > 250)
        {
            _text.text = $"B";
            AudioManager.Instance.PlaySFX(_ranks[2], 7.0f);
        }
        if (_value > 500)
        {
            _text.text = $"A";
            AudioManager.Instance.PlaySFX(_ranks[3], 7.0f);
        }
        if (_value > 1000)
        {
            _text.text = $"S";
            AudioManager.Instance.PlaySFX(_ranks[4], 5.0f);
        }
    }
}
