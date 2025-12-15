using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ResetPosition : Cons_CameraObstacle
{
    private float _speed;
    private AudioClip _clip;
    private AudioSource _source;
    private Vector3 _startPos;
    public Camera_ResetPosition(Fsm_Camera Fsm) : base(Fsm)
    {
        _camera = null;
        _clip = null;
        _source = null;
        _speed = 0.0f;
        _startPos= Vector3.zero;
    }
    public Camera_ResetPosition Speed(float Speed)
    {
        _speed = Speed;
        return this;
    }
    public Camera_ResetPosition Clip(AudioClip Clip)
    {
        _clip = Clip;
        return this;
    }
    public Camera_ResetPosition AudioSource(AudioSource Source)
    {
        _source = Source;
        return this;
    }
    public Camera_ResetPosition StartRotation(Vector3 Pos)
    {
        _startPos= Pos;
        return this;
    }
    public override void Enter()
    {
        _camera.DetectPlayer(false);
        EventManager.Trigger(EEvents.ResetDectection);
        _source.PlayOneShot(_clip);
        _camera.SetColor(_color, 500f);
    }
    public override void Execute()
    {
        if (_camera.SetTarget)
        {
            _fsm.SetNewBehaviour(ECameraBehaviours.watchingEntity);
        }
        Vector3 Pos = (_startPos - _camTransform.localEulerAngles).normalized * Time.deltaTime * _speed;
        _camTransform.localEulerAngles += Pos;
        Debug.Log(Vector3.Distance(_startPos,_camTransform.localEulerAngles));
        if (Vector3.Distance(_camTransform.localEulerAngles, _startPos) < 2.0f)
        {
            _camTransform.localEulerAngles = _startPos;
            _fsm.SetNewBehaviour(ECameraBehaviours.Base);
        }
    }
}
