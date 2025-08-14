using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    private void Awake()
    {
        if(!Instance)
            Instance = this;
        else
            DontDestroyOnLoad(Instance);
    }

    [SerializeField]private bool _isLoading = false;
    [SerializeField] private bool _animationIsPlaying;


    public void LoadSceneInMenu(string sceneName)
    {
        if (!_isLoading)
            StartCoroutine(LoadAsynInMenu(sceneName));
    }
    private IEnumerator LoadAsynInMenu(string sceneName)
    {
        _isLoading = true;
        _animationIsPlaying = true;
        AsyncOperation asynOp = SceneManager.LoadSceneAsync(sceneName);
        asynOp.allowSceneActivation = false;
        while (_animationIsPlaying|| asynOp.progress <0.9f) // si la escena esta antes de que termine la animacion, que espere hasta que la misma termine
        {
            yield return null;
        }
        asynOp.allowSceneActivation=true;// la escena no se actualiza hasta que cambias a otra ventana
        _isLoading = false;
        _animationIsPlaying = true;
}
    public void QuitApp()
    {
        Application.Quit();
    }

    public bool SetAnimationIsPlaying
    {
        get { return _animationIsPlaying; }
        set { _animationIsPlaying = value; }
    }
}
