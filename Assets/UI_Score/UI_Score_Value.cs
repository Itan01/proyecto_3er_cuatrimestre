using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Score_Value : MonoBehaviour, IObserverScore
{
    private TextMeshProUGUI _text;
    private float _score;
    private float _finalScore;
    Action VitualUpdate;
    private void Start()
    {
        GetComponentInParent<IObservableScore>().AddObs(this);
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = $"${_score:000}";
        VitualUpdate = null;
    }
    public void Execute(float Value, float AddValue)
    {
        _finalScore= Value + AddValue;
        VitualUpdate =UpdateScore;
    }
    private void Update()
    {
        VitualUpdate?.Invoke();
    }

    private void UpdateScore()
    {
        _score += _finalScore * Time.deltaTime;
        if (_score >=_finalScore)
        {
            _score = _finalScore;
            VitualUpdate = null;
        }
        _text.text = $"${(int)_score:000}";
    }
}
