using System;
using UnityEngine;
public class PlayerManager : AbstractPlayer
{
    private PlayerInputReader _inputReader;

    private Model_Player _model;
    private PL_Control _controller;
    private View_Player _view;

    [SerializeField] private bool _onCaptured = false;
    [Header("<color=red>Transform</color>")]
    [SerializeField] private Transform _megaphoneTransform;
    private float _invisibleDuration=0.0f;
    private bool _invisible = false;
    [SerializeField] private GameObject[] _skinModel;
    [SerializeField] private GameObject[] _clothesModel;
    [SerializeField] private Material _clothesBreathing;
    [SerializeField] private Material[] _baseColorMaterial;
   [SerializeField] private Material[] _JoinColorMaterial;
    [SerializeField] private AudioClip _deathClip; 
    [SerializeField] private AudioSource _source; 
    protected override void Awake()
    {
        GameManager.Instance.PlayerReference = this;
        LVLManager.Instance.Respawn = transform.position;
        _inputReader = GetComponent<PlayerInputReader>();
    }
    protected override void Start()
    {
        base.Start();
        _source=GetComponent<AudioSource>();
        _view = new View_Player(this);
        _model = new Model_Player(this);
        _controller = new PL_Control(this,_model, _view);
        EventManager.Subscribe(EEvents.DetectPlayer,SetCapturedTrue);
        EventManager.Subscribe(EEvents.Reset,Death);
        EventManager.Subscribe(EEvents.StartLVL, SetCapturedFalse);
        EventManager.Subscribe(EEvents.ReStart, ResetPosition);
        EventManager.Subscribe(EEvents.ResetDectection  , SetCapturedFalse);
    }

    protected override void Update()
    {
        GetStats();
        if (!_canControl) return;
        base.Update();
        _controller.Execute();
    }
    protected override void FixedUpdate()
    {
        if (!_canControl) return;
        _controller.FixedExecute();
    }
    private void GetStats()
    {
        if (_invisibleDuration > 0.0f)
        {
            _invisibleDuration -= Time.deltaTime;
            if (_invisibleDuration < 0.0f)
            {
                _invisibleDuration = 0.0f;
                _invisible = false;
                foreach (var item in _skinModel)
                {
                    item.GetComponent<SkinnedMeshRenderer>().material = _baseColorMaterial[0];
                }
                foreach (var item in _clothesModel)
                {
                    item.GetComponent<SkinnedMeshRenderer>().material = _JoinColorMaterial[0];
                }
                _clothesModel[1].GetComponent<SkinnedMeshRenderer>().material = _clothesBreathing;
            }
        }
        _isMoving = _model.GetIsMoving();
        _isCrouching =_controller.IsCrouching();
    }
    public bool IsCaptured
    {
        get { return _onCaptured; }
    }
    public bool GetInvisible()
    {
        return _invisible;
    }
    public void SetIfPlayerCanMove(bool state)
    {
        _canControl = state;
        if (!state && _animator !=null) _animator.SetBool("isMoving",false);
    }
    public void SetInvisiblePowerUp(float duration)
    {
        _invisibleDuration =duration;
        foreach (var item in _skinModel)
        {
            item.GetComponent<SkinnedMeshRenderer>().material = _baseColorMaterial[1];
        }
        foreach (var item in _clothesModel)
        {
            item.GetComponent<SkinnedMeshRenderer>().material = _JoinColorMaterial[1];
        }
        _invisible = true;  
    }
    private void SetCapturedTrue(params object[] Parameters)
    {
        _onCaptured = true;
    }
    private void SetCapturedFalse(params object[] Parameters)
    {
        _onCaptured = false;
    }
    private void ResetPosition(params object[] Parameters)
    {
        transform.position = LVLManager.Instance.Respawn;
        _animator.SetBool("isDeath", false);
    }
    private void Death(params object[] Parameters)
    {
        _source.PlayOneShot(_deathClip);
        _animator.SetBool("isDeath", true);
        _speed = 0.0f;
    }
    public void ForceCrouch(bool state)
    {
        if (_controller != null)
            _controller.ForceCrouch(state);
    }
    public void ResetPhysicsState()
    {
        _model.ResetPhysics();
    }
    private void OnDestroy()
    {
        EventManager.Unsubscribe(EEvents.DetectPlayer, SetCapturedTrue);
        EventManager.Unsubscribe(EEvents.Reset, Death);
        EventManager.Unsubscribe(EEvents.StartLVL, SetCapturedFalse);
        EventManager.Unsubscribe(EEvents.ReStart, ResetPosition);
        EventManager.Unsubscribe(EEvents.ResetDectection, SetCapturedFalse);
    }

}