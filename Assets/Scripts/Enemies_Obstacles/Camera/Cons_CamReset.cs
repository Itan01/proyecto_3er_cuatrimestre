using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Cons_CamReset : ICamMovement
{
    private AudioSource _audio;
    private Vector3 _camRotation;
    private Transform _camTransform;
    private bool _isResseted;
    private AudioClip _clip;
    private CameraObstacleController _script;
    private Cons_CamColorLight _camColorLight;
    private RoomManager _room;
    private float _speed;
    private float _xRotation, _yRotation, _zRotation;
    public Cons_CamReset(CameraObstacleController Script, Transform CamTransform,Renderer Render, Light Light,Color Color, AudioSource Audio,RoomManager Room, float Speed)
    {
        _camTransform= CamTransform;
        _audio = Audio;
        _script= Script;
        _speed= Speed;
        _room= Room;    
        _camColorLight = new Cons_CamColorLight(Render,Light, Color);
    }
    public void Move()
    {
        ResetRotation();
        if (!_isResseted)
        {
            _camTransform.localEulerAngles = new Vector3(_xRotation, _yRotation, _zRotation);
        }
        else
        {
            //if (_room.IsRoomActivate())
            //   _script.BaseCam();
        }

    }

    public void Setter()
    {
        if (_audio.isPlaying)
            _audio.Stop();
        _clip = AudioStorage.Instance.CameraSound(EnumAudios.CameraResetting);
        _audio.PlayOneShot(_clip);
        _camRotation = _camTransform.localEulerAngles;
        _xRotation = _camRotation.x;
        _yRotation = _camRotation.y;
        _zRotation = _camRotation.z;
        _camColorLight.SetCameraColor();
        _room.ResetDetection();
}
    private void ResetRotation()
    {
        _isResseted = true;
        _xRotation = SetValueToZero(_xRotation);
        _yRotation = SetValueToZero(_yRotation);
        _zRotation = SetValueToZero(_zRotation);
    }
    private float SetValueToZero(float number)
    {
        if (number > 0.25f && number < 180f)
        {
            number -= _speed * Time.deltaTime;
            _isResseted = false;
        }
        else if (number < 359.25f && number > 180f)
        {
            number += _speed * Time.deltaTime;
            _isResseted = false;
        }
        else
        {
            number = 0.0f;
        }
        return number;
    }
}
