using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Score_Value : MonoBehaviour, IObserverScore
{
    private TextMeshProUGUI _text;
    private float _score;
    private float _finalScore;
    private int _index = 1;
    [SerializeField] private AudioClip _clipMilestone;
    [SerializeField] private int _milestoneValue = 1000;
    [SerializeField] private Color[] _nextColors;
    Action VirtualUpdate;
    private void Start()
    {
        GetComponentInParent<IObservableScore>().AddObs(this);
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = $"${_score:000}";
        _text.color = _nextColors[_index];
        //_text.color = new Color(166, 159, 148);
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
        CheckMilestone();
    }

    private void CheckMilestone()
    {
        if (_score >= _index * _milestoneValue)
        {
            _index++;
            if (_clipMilestone != null) AudioManager.Instance.PlaySFX(_clipMilestone, 1.0f);
            if (_index >= _nextColors.Length) return;
            _text.color = _nextColors[_index];
        }
    }
}
