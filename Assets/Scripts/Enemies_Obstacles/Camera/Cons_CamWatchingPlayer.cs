using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.GraphicsBuffer;

public class Cons_CamWatchingPlayer : ICamMovement
{
    private AudioSource _audio;
    private AudioClip _clip;
    private Transform _camTransform;
    private Vector3 _target;
    private Cons_CamColorLight _colorLight;
    private Cons_Raycast _Raycast;
    private SO_Layers _layer;
    private RoomManager _room;
    private bool _detectingPlayer, _checkingPlayer;
    private CameraObstacleController _script;

    public Cons_CamWatchingPlayer(CameraObstacleController camScript, Transform CamTransform, Light Light, Renderer Render, Color Color, RoomManager Room, AudioSource Audio)
    {
        _audio = Audio;
        _script= camScript;
        _camTransform = CamTransform;
        _room = Room;
        _colorLight = new Cons_CamColorLight(Render, Light, Color);
        _Raycast = new Cons_Raycast(500f, _layer._everything);

    }

    public void Move()
    {
        //_target = GameManager.Instance.PlayerReference.GetHipsPosition();
        _camTransform.LookAt(_target);
        CheckTarget();
    }
    public void Setter()
    {
        if (_audio.isPlaying)
            _audio.Stop();
        _clip = AudioStorage.Instance.CameraSound(EAudios.CameraDetection);
        _audio.PlayOneShot(_clip);
        _room.DetectPlayer();
        _detectingPlayer = true;
        _colorLight.SetCameraColor();
    }

    public void CheckTarget()
    {
        if (GameManager.Instance.PlayerReference.GetInvisible()) 
        {
           // _script.ResetCam();
            return;
        }
        _checkingPlayer = false;
            Vector3 TargetHips = (GameManager.Instance.PlayerReference.GetHipsPosition().position - _camTransform.position).normalized;
            Vector3 TargetFeet = (GameManager.Instance.PlayerReference.transform.position - _camTransform.position).normalized;
            Vector3 TargetHead = (GameManager.Instance.PlayerReference.GetHeadPosition().position - _camTransform.position).normalized;
            if (_Raycast.Checker<PlayerManager>(_camTransform.position, TargetHips) ||
                _Raycast.Checker<PlayerManager>(_camTransform.position, TargetFeet) ||
                _Raycast.Checker<PlayerManager>(_camTransform.position, TargetHead))
                _checkingPlayer = true;
        if (_checkingPlayer)
        {
            if (!_detectingPlayer)
                Setter();
        }
        else
        {
            //if (_detectingPlayer)
            //    _script.ResetCam();
        }
    }
}
