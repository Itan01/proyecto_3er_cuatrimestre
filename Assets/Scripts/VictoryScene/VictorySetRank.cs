using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictorySetRank : MonoBehaviour
{
    private int _value;
    private TextMeshProUGUI _text;
    [SerializeField] private AudioSource _audioSource;
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
            _audioSource.PlayOneShot(_ranks[0],10.0f);
        }
        if (_value > 50)
        {
            _text.text = $"C";
            _audioSource.PlayOneShot(_ranks[1], 8.0f);
        }
        if (_value > 250)
        {
            _text.text = $"B";
            _audioSource.PlayOneShot(_ranks[2], 8.0f);
        }
        if (_value > 500)
        {
            _text.text = $"A";
            _audioSource.PlayOneShot(_ranks[3], 8.0f);
        }
        if (_value > 1000)
        {
            _text.text = $"S";
            _audioSource.PlayOneShot(_ranks[4], 5.0f);
        }
    }
}
