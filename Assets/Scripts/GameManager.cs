using System.Collections;
using System.Collections.Generic;
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
    private PlayerManager _playerGameObject;

    public PlayerManager PlayerReference
    {
        get { return _playerGameObject; }
        set { _playerGameObject = value; }
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

}
