using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Sigleton
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
       _menuPause= FindAnyObjectByType<s_pauseMenu>();
    }       

    #endregion
    private PlayerManager _player;
    [SerializeField] private Image _cooldownCircleBar;

    public Image CooldownCircleBar
    {
        get { return _cooldownCircleBar; }
        set {  _cooldownCircleBar = value; }
    }

    private CountdownTimer _timer;
    public CountdownTimer Timer
    {
        get { return _timer; }
        set { _timer = value; }
    }    
    
    private void Start()
    {
        _player = GameManager.Instance.PlayerReference;
        StartCoroutine(PlayAnimationAfterLoad());
    }
    [SerializeField] private s_pauseMenu _menuPause;
    public s_pauseMenu MenuPause
    {
        get { return _menuPause; }
        set { _menuPause = value; }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuON)
                Resume();
            else
                Pause();
        }
    }

    public bool IsMenuActive()
    {
        return isMenuON;
    }
    public void SetMenuFalse()
    {
        isMenuON=false;
    }

    private TransitionFade _scriptTransition;
    public TransitionFade Transition
    {
        get { return _scriptTransition; }
        set { _scriptTransition = value; }
    }

    private UI_Sound _soundUI;
    public UI_Sound UISound
    {
        get { return _soundUI; }
        set { _soundUI = value; }
    }



    private UI_Aim _aimUI;

    public UI_Aim AimUI
    {
        get { return _aimUI; }
        set { _aimUI = value; }
    }


    [SerializeField] private int _score = 0;
    public int GetScore()
    {
        return _score;
    }
    [SerializeField] private GameObject _popupPoints;
    private int _displayedScore = 0;

    private s_UI_FinishLevel _finishLevelComic; 

    public s_UI_FinishLevel FinishLevelComic
    {
        get { return _finishLevelComic; }
        set { _finishLevelComic = value; }
    }

    public void FinishLevel()
    {
        _player.SetIfPlayerCanMove(false, true);
        _finishLevelComic.gameObject.SetActive(true);
    }

    public string transAnimation;
    public Animator animator;

    private IEnumerator PlayAnimationAfterLoad()
    {
        yield return null;

        if (animator != null && !string.IsNullOrEmpty(transAnimation))
        {
            animator.Play(transAnimation);
        }

    }

    private IEnumerator ClearInputBuffer()
    {
        yield return null;
        Input.ResetInputAxes();
    }

    private bool isMenuON;
    public void Resume()
    {
        _menuPause.SetActivate(false);
        Time.timeScale = 1f; // Descongela La Pantalla
        isMenuON = false; 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Limpia los inputs 
        StartCoroutine(ClearInputBuffer());
    }
    private void Pause()
    {
        _menuPause.SetActivate(true);
        StartCoroutine(EnableMenuSafely());
        Time.timeScale = 0f;         // Congela La Pantalla 
        isMenuON = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    private IEnumerator EnableMenuSafely()
    {
        yield return null; // Espera 1 frame 
        Time.timeScale = 0f;
        isMenuON = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    [SerializeField] private float _finalTimer = 0f;
    public float FinalTime
    { 
        get { return _finalTimer; }
        set { _finalTimer = value; }
    }
}