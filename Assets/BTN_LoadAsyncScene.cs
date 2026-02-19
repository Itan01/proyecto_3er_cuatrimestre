using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTN_LoadAsyncScene : MonoBehaviour
{
    [SerializeField] private string _scene;
    public void LoadAsyncScene()
    {
        StartCoroutine(Loading());
    }
    private IEnumerator Loading()
    {
        AsyncOperation NewScene = SceneManager.LoadSceneAsync(_scene);
        NewScene.allowSceneActivation = false;
        while(NewScene.progress < 0.9f)
        {
            yield return null;
        }
        NewScene.allowSceneActivation = true;
    }
}
