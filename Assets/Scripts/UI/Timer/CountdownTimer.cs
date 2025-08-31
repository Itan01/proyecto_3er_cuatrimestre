using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{ 

    [SerializeField] private float _timer = 0f;
    [SerializeField] private TextMeshProUGUI _timerText;
    private bool _isRunning = true;
    private void Awake()
    {
        UIManager.Instance.Timer = this;
    }

    private void Start()
    {
        AudioStorage.Instance.UiSound(EnumAudios.Timer);
    }
    void Update()
    {
        if (!_isRunning) return;

        _timer += Time.deltaTime;
        UpdateTimerUI(_timer);

    }

    private void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        //int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
        _timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void StopTimerUI() 
    {
        GameManager.Instance.TimeOnlevel = Mathf.FloorToInt(_timer);
        _isRunning = false;

    }

    public void IsRunning(bool state)
    {
        _isRunning = state;
    }

}
