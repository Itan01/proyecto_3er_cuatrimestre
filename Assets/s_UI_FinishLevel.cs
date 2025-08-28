using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_UI_FinishLevel : MonoBehaviour
{ 
    public void SetNextScene()
    {
        SetValuesToGameManager();
        StartCoroutine(TransitionsToPoints());
    }
    private IEnumerator TransitionsToPoints()
    {
      AsyncOperation async =  SceneManager.LoadSceneAsync("Victory");
        async.allowSceneActivation = false;
        while (async.progress <0.9f)
        {
            yield return null;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        async.allowSceneActivation = true;

    }
    private void SetValuesToGameManager()
    {
        GameManager.Instance.ScoreValue = UIManager.Instance.GetScore();
        //Veces caputarada ya estan en el mismo GameManager
        UIManager.Instance.Timer.StopTimerUI();
    }
}
