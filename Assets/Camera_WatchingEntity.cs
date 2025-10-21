using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Camera_WatchingEntity : Cons_CameraObstacle
{
    private Transform _targetTransform;
    public Camera_WatchingEntity(Fsm_Camera Fsm) : base(Fsm)
    {
    }
    public Camera_WatchingEntity Target(Transform Transform)
    {
        _targetTransform = Transform;
        return this;
    }
    public override void Enter()
    {
        Debug.Log("Enter To WatchMode");
        _camera.SetColor(_color,9999f);
        EventManager.Trigger(EEvents.DetectPlayer, _targetTransform);
    }
    public override void Execute()
    {
        _camTransform.LookAt(_targetTransform);
        if(_camera.SetTarget== false)
        {
            _fsm.SetNewBehaviour(ECameraBehaviours.Base);
        }
    }
    public override void FixedExecute()
    {
        base.FixedExecute();
    }
}
