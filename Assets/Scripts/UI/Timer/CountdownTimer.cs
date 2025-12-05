using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{ 

    [SerializeField] private float _timer = 0f;
    private TextMeshProUGUI _timerText;
    private AudioClip _timerEffect;
    private float _timerWait;
    [SerializeField] private bool _isRunning = false;
   public delegate void BehaviourTimer();
    public BehaviourTimer TimerUpdate;
    private void Awake()
    {
        UIManager.Instance.Timer = this;
    }
    private void Start()
    {
        if(!GameManager.Instance.FirstTimePlay) _isRunning = true;
        _timerText =GetComponentInChildren<TextMeshProUGUI>();
        _timerEffect = AudioStorage.Instance.UiSound(EAudios.Timer);
        UIManager.Instance.Timer = this;
        TimerUpdate = TimerSubstract;
        // AudioManager.Instance.PlaySFX(_timerEffect, 1f);
    }
    void Update()
    {
        if (!_isRunning) return;
        TimerUpdate?.Invoke();


    }
    private void TimerSubstract()
    {
        if (_timer < 0)
            TimerUpdate = TimerOff;
        _timer -= Time.deltaTime;
        if (_timerWait > 1000)
        {
            AudioManager.Instance.PlaySFX(_timerEffect, 1f);
            _timerWait = 0f;
        }
        UpdateTimerUI(_timer);
    }
    private void TimerOff()
    {
        _timerText.color = Color.red;
        _timerText.text = $"-- : --";
        TimerUpdate = null;
    }
    private void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        _timerText.text = $"{minutes:00}:{seconds:00}";
    }
    public void StopTimerUI() 
    {
        GameManager.Instance.FinalTimeOnLVL = Mathf.FloorToInt(_timer);
        _isRunning = false;

    }

    public void IsRunning(bool state)
    {
        _isRunning = state;
    }

}
