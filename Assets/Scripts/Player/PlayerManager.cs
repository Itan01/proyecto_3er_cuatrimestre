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
    [SerializeField] private Material[] _baseColorMaterial;
   [SerializeField] private Material[] _JoinColorMaterial;
    protected override void Awake()
    {
        GameManager.Instance.PlayerReference = this;
        LVLManager.Instance.Respawn = transform.position;
        _inputReader = GetComponent<PlayerInputReader>();
    }
    protected override void Start()
    {
        base.Start();
        _view = new View_Player(this);
        _model = new Model_Player(this);
        _controller = new PL_Control(_model, _view);
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
        //if (_invisibleDuration > 0.0f)
        //{
        //    _invisibleDuration -= Time.deltaTime;
        //    if (_invisibleDuration < 0.0f)
        //    {
        //        _invisibleDuration = 0.0f;
        //        _invisible = false;
        //        foreach (var item in _skinModel) 
        //        {
        //            item.GetComponent<SkinnedMeshRenderer>().material = _baseColorMaterial[0];
        //        }
        //        foreach (var item in _clothesModel)
        //        {
        //            item.GetComponent<SkinnedMeshRenderer>().material = _JoinColorMaterial[0];
        //        }
        //    }
        //}
        _isMoving = _model.GetIsMoving();
        _isCrouching =_model.GetIsCrouching();
    }
    public bool IsCaptured
    {
        get { return _onCaptured; }
        set { _onCaptured = value; }
        
    }
    public bool GetInvisible()
    {
        return _invisible;
    }
    public void SetIfPlayerCanMove(bool state, bool FinishingLevel)
    {
        _canControl = state;
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

}