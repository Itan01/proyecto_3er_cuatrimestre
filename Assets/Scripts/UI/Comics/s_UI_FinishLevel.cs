using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_UI_FinishLevel : MonoBehaviour
{
    private Animator _animator;
    private bool _isTransitioning=false;
    private AudioClip _clipAlert;
    [SerializeField]private UI_Score _score;
    private void Start()
    {
        _clipAlert = AudioStorage.Instance.StandardEnemySound(EAudios.EnemyAlert);
        _animator = GetComponent<Animator>();
        UIManager.Instance.FinishLevelComic = this;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if(Input.anyKey && !_isTransitioning)
        {
            NextTransition();
        }
    }
    public void SetNextScene()
    {
        StartCoroutine(TransitionsToPoints());
    }
    private IEnumerator TransitionsToPoints()
    {
      AsyncOperation async =  SceneManager.LoadSceneAsync("Victory");
        SetValuesToGameManager();
        async.allowSceneActivation = false;
        while (async.progress <0.9f)
        {
            yield return null;
        }
        while (!Input.anyKey)
        {
            yield return null;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        async.allowSceneActivation = true;

    }
    private void SetValuesToGameManager()
    {
        GameManager.Instance.FinalScore = _score.GetScore();
        //Veces caputarada ya estan en el mismo GameManager
        UIManager.Instance.Timer.StopTimerUI();
    }
    private void NextTransition()
    {
        _isTransitioning = true;
        _animator.SetTrigger("Transition");
    }
    public void FinishTransition()
    {
        _isTransitioning=false;
    }
    public void playAlert() 
    {
        AudioManager.Instance.PlaySFX(_clipAlert, 1.0f);
    }
}
