using System;
using UnityEngine;
public class PlayerManager : AbstractPlayer
{
    //private Control_GrabbingSound _areaCatching;
    private PlayerInputReader _inputReader;

    private Model_Player _model;
    private PL_Control _controller;
    private View_Player _view;

    [SerializeField] private bool _onCaptured = false;
    [Header("<color=green>LayersMask</color>")]
    [SerializeField] private LayerMask _soundMask;
    [Header("<color=red>Transform</color>")]
    [SerializeField] private Transform _spawnProyectil;
    [SerializeField] private Transform _megaphoneTransform;

    private float _invisibleDuration=0.0f;
    private bool _invisible = false;
    [SerializeField] private GameObject _particlesInvisible;
    [SerializeField] private GameObject[] _skinModel;
    [SerializeField] private GameObject[] _clothesModel;
    [SerializeField] private Material[] _baseColorMaterial;
   [SerializeField] private Material[] _JoinColorMaterial;

    [SerializeField] private AudioClip _cadenzaDead;
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
        //_areaCatching = GetComponentInChildren<Control_GrabbingSound>();
        SetAreaCatching(false);
    }

    protected override void Update()
    {
        if (!_canControl) return;
        base.Update();
        _controller.Execute();
    }
    protected override void FixedUpdate()
    {
        if (!_canControl) return;
        _controller.FixedExecute();
    }
    public void SetSound(int Index)
    {
        SoundStruct aux = GameManager.Instance.SoundsReferences.GetSoundComponents(Index);
    }
    public void SetAreaCatching(bool State)
    {
        //_areaCatching.gameObject.SetActive(State);
    }
    private void GetStats()
    {
        if (_invisibleDuration > 0.0f)
        {
            _invisibleDuration -= Time.deltaTime;
            if (_invisibleDuration < 0.0f)
            {
                _particlesInvisible.SetActive(false);
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
            }
        }
    }
    public void SetCaptured(bool State)
    {
        _onCaptured = State;
    }
    public bool GetCaptured()
    {
        return _onCaptured;
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
        _particlesInvisible.SetActive(true);
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