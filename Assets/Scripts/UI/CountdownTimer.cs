using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float _timeLeft = 2400f;
    [SerializeField] private TextMeshProUGUI _timerText;
    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        if (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            UpdateTimerUI(_timeLeft);
            if (_timeLeft < 60f)
                _timerText.color = Color.red;
        }
        else
        {
            _timeLeft = 0;
            isRunning = false;
            UpdateTimerUI(_timeLeft);
            GameManager.Instance.AlertEnemies();
        }
    }

    private void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        _timerText.text = $"{minutes:00}:{seconds:00}";
    }

   
}
