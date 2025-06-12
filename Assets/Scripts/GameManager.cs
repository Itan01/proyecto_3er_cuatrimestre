using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    private PlayerManager _player;
    private List<AbstractEnemy> _enemies = new List<AbstractEnemy>();

    public PlayerManager PlayerReference
    {
        get { return _player; }
        set { _player = value; }
    }

    private SoundReferences _soundRef;
    public SoundReferences SoundsReferences
    {
        get { return _soundRef; }
        set {  _soundRef = value; }
    }


    public void RegisterEnemy(AbstractEnemy enemy)
    {
        _enemies.Add(enemy);
    }

    private Vector3 _positionToRespwan;
    public Vector3 RespawnReference
    {
        get { return _positionToRespwan; }
        set { _positionToRespwan = value; }
    }

    public void RespawnAllEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Respawn();
        }
    }
    private Transform _camera;
    public Transform CameraReference
    {
        get { return _camera; }
        set { _camera = value; }
    }

    private int _score = 0;
    private TMP_Text _pointsUI;
    public TMP_Text TextReference
    {
        get { return _pointsUI; }
        set { _pointsUI = value; }
    }
    private string _mainText = "Gold: ";
    public int SetScore
    {
        get { return _score; }
        set { ChangeScore(value); }
    }

    private void ChangeScore(int value)
    {
        _score += value;
        _pointsUI.text = ($"{_mainText}{_score}");
    }

    private UISetSound _soundUI;
    public UISetSound UISound
    {
        get { return _soundUI; }
        set { _soundUI=value; }
    }


    private TuriorialFirstTime _textFirstTime;

    public TuriorialFirstTime FirstTimeReference
    {
        get { return _textFirstTime; }
        set { _textFirstTime = value; }
    }
}
