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
    private void Start()
    {
    }
    public PlayerManager PlayerReference
    {
        get { return _player; }
        set { _player = value; }
    }

    private SoundReferences _soundRef;
    public SoundReferences SoundsReferences
    {
        get { return _soundRef; }
        set { _soundRef = value; }
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

    //private void ChangeScore(int value)
    //{
    //    _score += value;
    //    _pointsUI.text = ($"{_mainText}{_score}");
    //}

    private TuriorialFirstTime _textFirstTime;

    public TuriorialFirstTime FirstTimeReference
    {
        get { return _textFirstTime; }
        set { _textFirstTime = value; }
    }


    private RoomManager _actualRoom;

    public RoomManager Room
    {
        get { return _actualRoom; }
        set { _actualRoom = value; }
    }
    public void ResetGameplay()
    {
        StartCoroutine(ResetTImer());
    }
    private IEnumerator ResetTImer()
    {
        PlayerReference.SetDeath(true);
        UIManager.Instance.Transition.ShowBlackScreen();
        yield return new WaitForSeconds(1.0f);
        Room.ResetRoom();
        PlayerReference.SetCaptured(false);
        PlayerReference.SetDeath(false);
        UIManager.Instance.Transition.FadeOut();
        PlayerReference.transform.position = RespawnReference;
    }
}
