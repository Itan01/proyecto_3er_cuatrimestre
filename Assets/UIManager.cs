using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Sigleton
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;

    }

    #endregion
    private PlayerManager _player;

    private void Start()
    {
        _player = GameManager.Instance.PlayerReference;
    }
    private TransitionFade _scriptTransition;
    public TransitionFade Transition
    {
        get { return _scriptTransition; }
        set { _scriptTransition = value; }
    }

    private UISetSound _soundUI;
    public UISetSound UISound
    {
        get { return _soundUI; }
        set { _soundUI = value; }
    }



    private AimManagerUI _aimUI;

    public AimManagerUI AimUI
    {
        get { return _aimUI; }
        set { _aimUI = value; }
    }


    private int _score = 0;
    private TMP_Text _pointsUI;
    public TMP_Text TextReference
    {
        get { return _pointsUI; }
        set { _pointsUI = value; }
    }
    private string _mainText = "$";
    public void AddScore(int amount)
    {
        _score += amount;

        if (_scoreCoroutine != null)
            StopCoroutine(_scoreCoroutine);

        _scoreCoroutine = StartCoroutine(AnimateScore());
    }
    private int _displayedScore = 0;
    private Coroutine _scoreCoroutine;
    private IEnumerator AnimateScore()
    {
        while (_displayedScore < _score)
        {
            _displayedScore++;
            _pointsUI.text = $"{_mainText}{_displayedScore}";
            yield return new WaitForSeconds(0.00001f);
        }
    }
}
