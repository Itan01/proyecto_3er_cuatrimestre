using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Camera_WatchingEntity : Cons_CameraObstacle
{
    private AudioSource _source;
    private AudioClip _clip;
    private Transform _targetTransform;
    public Camera_WatchingEntity(Fsm_Camera Fsm) : base(Fsm)
    {
        _clip = null;
        _source = null;
        _targetTransform = null;
    }
    public Camera_WatchingEntity Target(Transform Transform)
    {
        _targetTransform = Transform;
        return this;
    }
    public Camera_WatchingEntity Clip(AudioClip Clip)
    {
        _clip = Clip;
        return this;
    }
    public Camera_WatchingEntity AudioSource(AudioSource Source)
    {
        _source = Source;
        return this;
    }
    public override void Enter()
    {
        _camera.SetColor(_color,9999f);
        _source.PlayOneShot(_clip);
        EventManager.Trigger(EEvents.DetectPlayer, _targetTransform);
    }
    public override void Execute()
    {
        _camTransform.LookAt(_targetTransform);
        if(!_camera.SetTarget)
        {
            _fsm.SetNewBehaviour(ECameraBehaviours.Reset);
        }
    }
    public override void FixedExecute()
    {
        base.FixedExecute();
    }
}
