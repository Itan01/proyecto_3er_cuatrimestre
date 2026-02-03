using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Score_Value : MonoBehaviour, IObserverScore
{
    private TextMeshProUGUI _text;
    private float _score;
    private float _finalScore;
    Action VirtualUpdate;
    private void Start()
    {
        GetComponentInParent<IObservableScore>().AddObs(this);
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = $"${_score:000}";
        _text.color = new Color(166, 159, 148);
        VirtualUpdate = null;
    }
    public void Execute(float Value, float AddValue)
    {
        _finalScore= Value + AddValue;
        VirtualUpdate =UpdateScore;
    }
    private void Update()
    {
        VirtualUpdate?.Invoke();
    }

    private void UpdateScore()
    {
        _score += _finalScore * Time.deltaTime;
        if (_score >=_finalScore)
        {
            _score = _finalScore;
            VirtualUpdate = null;
        }
        _text.text = $"${(int)_score:000}";
        if(_score >= 50) 
        {
            _text.color = new Color(185, 134, 59);
        }
    }
}
