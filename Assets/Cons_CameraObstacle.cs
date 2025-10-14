using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cons_CameraObstacle : ICameraObstacle
{
    private Dictionary<ECameraBehaviours, ICameraObstacle> _behaviours = new Dictionary<ECameraBehaviours, ICameraObstacle>();
    protected Fsm_Camera _fsm;
    protected Transform _camTransform;
    protected Camera_Obstacle _camera;
    protected Color _color;
    public Cons_CameraObstacle(Fsm_Camera Fsm) 
    {
        _fsm = Fsm;
    }
    public virtual void AddBehaviour(ECameraBehaviours BehaviourName, ICameraObstacle State)
    {
        if (!_behaviours.ContainsKey(BehaviourName))
        {
            _behaviours.Add(BehaviourName, State);
        }
    }
    public virtual void Enter() 
    {
    }

    public virtual void Execute()
    {
    }

    public  virtual void Exit()
    {
    }

    public virtual void FixedExecute()
    {
       
    }
    public virtual bool GetBehaviour(ECameraBehaviours Key, out ICameraObstacle State)
    {

        if (_behaviours.ContainsKey(Key))
        {
            State = _behaviours[Key];
            return  true;
        }
        else
        {
            State = null;
            return false;
        }
            
    }
    public Cons_CameraObstacle CamTransform(Transform CamTransform)
    {
        _camTransform = CamTransform;
        return this;
    }
    public Cons_CameraObstacle Camera(Camera_Obstacle Camera)
    {
        _camera = Camera;
        return this;
    }
    public Cons_CameraObstacle Color(Color Color)
    {
        _color = Color;
        return this;
    }

}
