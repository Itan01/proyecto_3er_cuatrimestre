using UnityEngine;

public class Camera_Disabled: Cons_CameraObstacle
{
    private float _timer;
    public Camera_Disabled(Fsm_Camera Fsm) : base(Fsm)
    {
        _camera = null;
    }
    public override void Enter()
    {
        Debug.Log("Enter To DisableMode");
        _timer = 5.0f;
        _camera.SetColor(_color, 0.0f);
    }
    public override void Execute()
    {
       if(_timer <= 0.0f)
        {
            _fsm.SetNewBehaviour(ECameraBehaviours.Reset);
        }
        _timer -= Time.deltaTime;
    }
    public override void FixedExecute()
    {

    }
    public override void Exit() 
    {
        Debug.Log("Exit To DisableMode");
    }

}