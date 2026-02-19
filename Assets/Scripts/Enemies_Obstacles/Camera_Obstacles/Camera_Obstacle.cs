using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;
public class Camera_Obstacle : MonoBehaviour, ISoundAim, ISoundInteractions
{
    private Fsm_Camera _fsm;
    private Camera_BaseMovement _base;
    private Camera_WatchingEntity _watching;
    private Camera_ResetPosition _reset;
    private Camera_Disabled _disable;
    private RoomManager _room;
    private AudioSource _source;
    [SerializeField] private Transform _camTransform;
    [SerializeField] private bool _seeTarget;
    [SerializeField] private bool _isRunning=false;
    private Light _light;
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _detectorColor;
    [SerializeField] private Renderer _cameraLight;
    [SerializeField] private SO_Layers _layer;
    [SerializeField] private AudioClip _clipDetectPlayer;
    [SerializeField] private AudioClip _clipResetting;
    [SerializeField] private ScriptableRendererFeature _renderFullScreen;
    [SerializeField] private Material _materialFullScreen;
    [SerializeField] private VisualEffect[] _states;
    [SerializeField] private MeshRenderer _mesh;
    private void Start()
    {
        _room=GetComponentInParent<RoomManager>();
        _room.ActRoom += Activate;
        _room.DesActRoom += DesActivate;
        _light =GetComponentInChildren<Light>();
        _source= GetComponent<AudioSource>();
        _fsm = new Fsm_Camera();

        _base = (Camera_BaseMovement)new Camera_BaseMovement(_fsm).Camera(this).CamTransform(_camTransform).Color(_baseColor);
        _base = _base.Speed(10.0f).Rotation(35.0f);
        _watching= (Camera_WatchingEntity)new Camera_WatchingEntity(_fsm).Camera(this).CamTransform(_camTransform).Color(_detectorColor);
        _watching = _watching.Target(GameManager.Instance.PlayerReference.transform).AudioSource(_source).Clip(_clipDetectPlayer);
        _reset = (Camera_ResetPosition)new Camera_ResetPosition(_fsm).Camera(this).CamTransform(_camTransform).Color(_baseColor);
        _reset = _reset.Speed(12.5f).AudioSource(_source).Clip(_clipResetting);
        _disable = (Camera_Disabled)new Camera_Disabled(_fsm).Camera(this).Color(Color.gray);

        _base.AddBehaviour(ECameraBehaviours.watchingEntity, _watching);
        _base.AddBehaviour(ECameraBehaviours.Disable,_disable);

        _watching.AddBehaviour(ECameraBehaviours.Reset, _reset);
        _watching.AddBehaviour(ECameraBehaviours.Disable, _disable);

        _reset.AddBehaviour(ECameraBehaviours.Base,_base);
        _reset.AddBehaviour(ECameraBehaviours.watchingEntity, _watching);
        _reset.AddBehaviour(ECameraBehaviours.Disable, _disable);

        _disable.AddBehaviour(ECameraBehaviours.Reset, _reset);
        DetectPlayer(false);

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


    private void OnDestroy()
    {
        _room.ActRoom -= Activate;
        _room.DesActRoom -= DesActivate;
    }
    #region Modifiers
    public void SetColor(Color color, float intensity)
    {
        _cameraLight.material.SetColor("_Color", color);
        _cameraLight.material.SetColor("_EmissionColor", color);
        _light.color = color;
        _light.intensity = intensity;
    }
    public void DetectPlayer(bool State)
    {
        if (State)
        {
            _states[1].gameObject.SetActive(true);
            _states[0].gameObject.SetActive(false);
        }
        else
        {
            _states[1].gameObject.SetActive(false);
            _states[0].gameObject.SetActive(true);
        }
        _renderFullScreen.SetActive(State);
    }
    #endregion
    #region Getters&Setters
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

    public void Aim_Activate()
    {
        if (_mesh == null) return;
        _mesh.material.SetFloat("_Has_Aimed",1.0f);
    }

    public void Aim_Deactivate()
    {
        if (_mesh == null) return;
        _mesh.material.SetFloat("_Has_Aimed", 0.0f);
    }
    #endregion
    public void IIteraction(bool PlayerShootIt)
    {
        _states[1].gameObject.SetActive(false);
        _states[0].gameObject.SetActive(false);
        _fsm.SetNewBehaviour(ECameraBehaviours.Disable);
    }
}

