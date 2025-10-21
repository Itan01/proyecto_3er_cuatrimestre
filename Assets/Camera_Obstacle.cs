using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Camera_Obstacle : MonoBehaviour
{
    private Fsm_Camera _fsm;
    private Camera_BaseMovement _base;
    private Camera_WatchingEntity _watching;
    private RoomManager _room;
    [SerializeField] private Transform _camTransform;
    [SerializeField] private bool _seeTarget;
    [SerializeField] private bool _isRunning=false;
    private Light _light;
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _detectorColor;
    [SerializeField] private Renderer _cameraLight;
    [SerializeField] private SO_Layers _layer;
    private void Start()
    {
        _room=GetComponentInParent<RoomManager>();
        _room.ActRoom += Activate;
        _room.DesActRoom += DesActivate;
        _light =GetComponentInChildren<Light>();
        _fsm = new Fsm_Camera();
        _base = (Camera_BaseMovement)new Camera_BaseMovement(_fsm).Camera(this).CamTransform(_camTransform).Color(_baseColor);
        _base = _base.Speed(10.0f).Rotation(35.0f);
        _watching= (Camera_WatchingEntity)new Camera_WatchingEntity(_fsm).Camera(this).CamTransform(_camTransform).Color(_detectorColor);
        _watching = _watching.Target(GameManager.Instance.PlayerReference.transform);   
        _base.AddBehaviour(ECameraBehaviours.watchingEntity, _watching);
        _watching.AddBehaviour(ECameraBehaviours.Base, _base);
    }
    private void Update()
    {
        if (!_isRunning) return;
        _fsm.VirtualUpdate();
    }
    private void FixedUpdate()
    {
        if (!_isRunning) return;
        _fsm.VirtualFixedUpdate();
    }

    public bool SetTarget
    {
        get { return _seeTarget; }
        set { _seeTarget = value; }
    }
    private void Activate()
    {
        _fsm.SetStartBehaviour(_base);
        _isRunning = true;
    } 
    private void DesActivate()
    {
        _isRunning = false;
    }
    public void SetColor(Color color, float intensity)
    {
        _cameraLight.material.SetColor("_Color", color);
        _cameraLight.material.SetColor("_EmissionColor", color);
        _light.color = color;
        _light.intensity = intensity;
    }
    private void OnDestroy()
    {
        _room.ActRoom -= Activate;
        _room.DesActRoom -= DesActivate;
    }
}
