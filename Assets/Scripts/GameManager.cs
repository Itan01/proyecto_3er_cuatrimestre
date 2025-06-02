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

    public PlayerManager PlayerReference
    {
        get { return _player; }
        set { _player = value; }
    }

    private Vector3 _positionToRespwan;
    public Vector3 RespawnReference
    {
        get { return _positionToRespwan; }
        set { _positionToRespwan = value; }
    }

    private Transform _camera;
    public Transform CameraReference
    {
        get { return _camera; }
        set { _camera = value; }
    }

    private int _score=0;
    private TMP_Text _pointsUI;
    public TMP_Text TextReference
    {
        get { return _pointsUI; }
        set { _pointsUI=value; }
    }
    private string _mainText = "Gold: ";
    public int SetScore
    {
        get { return _score; }
        set{ ChangeScore(value); }
    }

    private void ChangeScore(int value)
    {
        _score += value;
        _pointsUI.text = ($"{_mainText}{_score}");
    }

}
