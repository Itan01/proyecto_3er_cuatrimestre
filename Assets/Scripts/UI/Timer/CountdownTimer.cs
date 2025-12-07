using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{ 

    [SerializeField] private float _timer = 0f;
    private TextMeshProUGUI _timerText;
    private AudioClip _timerEffect;
    private float _timerWait;
    private bool _noTime = false;
    [SerializeField] private bool _isRunning = false;
   public delegate void BehaviourTimer();
    public BehaviourTimer TimerUpdate;
    private void Start()
    {
        if(!GameManager.Instance.FirstTimePlay) _isRunning = true;
        _timerText =GetComponentInChildren<TextMeshProUGUI>();
        _timerEffect = AudioStorage.Instance.UiSound(EAudios.Timer);
        UIManager.Instance.Timer = this;
        TimerUpdate = TimerSubstract;
        EventManager.Subscribe(EEvents.ReStart, TimerOff);
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
        _noTime = true;
        EventManager.Trigger(EEvents.AlertPlayer,GameManager.Instance.PlayerReference.transform);
        _timerText.color = Color.red;   
        _timerText.text = $"00:00";
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
    public void TimerOff(params object[] Parameters)
    {
        if (!_noTime) return;
        StartCoroutine(LoadScene());
        
    }
    private IEnumerator LoadScene() 
    {
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync("Game_Over");
        loadingScene.allowSceneActivation = false;
        while (loadingScene.progress < 0.9f)
        {
            yield return null;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        loadingScene.allowSceneActivation = true;
        yield return null;
    }
    private void OnDestroy()
    {
        EventManager.Unsubscribe(EEvents.ReStart, TimerOff);
    }

}
