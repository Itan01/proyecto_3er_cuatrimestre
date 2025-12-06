using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    /*Variable Globales que se necesita entre Escenas*/
    #region Sigleton
    public static GameManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    #endregion
    [SerializeField]private PlayerManager _player;

    private bool _firstTimePlaying=true;
    public bool FirstTimePlay 
    {
        get { return _firstTimePlaying; }
        set { _firstTimePlaying = value; }
    }

    public PlayerManager PlayerReference
    {
        get { return _player; }
        set { _player = value; }
    }
    private Transform _camera;
    public Transform CameraReference
    {
        get { return _camera; }
        set { _camera = value; }
    }

    private TuriorialFirstTime _textFirstTime;

    public TuriorialFirstTime FirstTimeReference
    {
        get { return _textFirstTime; }
        set { _textFirstTime = value; }
    }
    [SerializeField] private int _time;

    public int FinalTimeOnLVL
    {
        get { return _time; }
        set { _time = value; }
    }

    [SerializeField] private int _score;

    public int FinalScore
    {
        get { return _score; }
        set { _score = value; }
    }

    [SerializeField] private int _timeCaptured = 0;

    public int TimesCaptured
    {
        get { return _timeCaptured; }
        set { _timeCaptured = value; }
    }

}
