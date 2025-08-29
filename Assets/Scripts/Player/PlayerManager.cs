using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class PlayerManager : EntityMonobehaviour
{
    private SetSizeCollider _scriptCollider;
    private PlayerController _scriptController;
    private PlayerMovement _scriptMovement;
    private PlayerShootingGun _scriptShootingGun;
    private PlayerInteractions _scriptInteractions;
    private GrabbingSound _areaCatching;
    private PlayerInputReader _inputReader;
    private PlayerDash _scriptDash;

    [SerializeField] private bool _onCaptured = false;
    [Header("<color=green>LayersMask</color>")]
    [SerializeField] private LayerMask _soundMask;
    [Header("<color=red>Transform</color>")]
    [SerializeField] private Transform _camTransform;
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private Transform _spawnProyectil;
    [SerializeField] private Transform _hipsPosition;
    [SerializeField] private Transform _megaphoneTransform;

    private float _invisibleDuration=0.0f;
    private bool _invisible = false;
    [SerializeField] private GameObject _particlesInvisible;
    [SerializeField] private GameObject[] _skinModel;
    [SerializeField] private GameObject[] _clothesModel;
    [SerializeField] private Material[] _baseColorMaterial;
   [SerializeField] private Material[] _JoinColorMaterial;
    public event Action SubtractTimer;
    private bool _HasNoControl=true;

    protected override void Awake()
    {
        GameManager.Instance.PlayerReference = this;
        GameManager.Instance.RespawnReference = transform.position;
        _inputReader = GetComponent<PlayerInputReader>();
    }
    protected override void Start()
    {
        _isThisPlayer = true;
        _areaCatching = GetComponentInChildren<GrabbingSound>();
        SetAreaCatching(false);
        base.Start();
    }

    protected override void Update()
    {
        if (_isDeath || !_HasNoControl) return;
        base.Update();
        CheckInputs();
        if(SubtractTimer!= null)
        SubtractTimer();
        GetStats();
    }
    protected override void FixedUpdate()
    {
        _scriptMovement.Move();
    }
    private void CheckInputs()
    {
        if (UIManager.Instance.IsMenuActive()) return; // bloquea inputs si el menú está activo
        _scriptController.Inputs();
    }
    protected override void GetComponents()
    {
        base.GetComponents();
        _scriptCollider = new SetSizeCollider(_capsuleCollider, _boxCollider);
        _scriptShootingGun = new PlayerShootingGun(_megaphoneTransform, _camTransform, _modelTransform);
        _scriptInteractions = new PlayerInteractions(_animator);
        _scriptDash = new PlayerDash(_modelTransform,_rb,_animator);
        _scriptMovement = new PlayerMovement(transform, _rb, _camTransform, _modelTransform, _animator, _scriptCollider);
        _scriptController = new PlayerController(_scriptShootingGun, _scriptInteractions, _scriptDash,_scriptMovement,_animator, _areaCatching);

    }
    public void SetSound(int Index)
    {
        SoundStruct aux = GameManager.Instance.SoundsReferences.GetSoundComponents(Index);
        _scriptShootingGun.SetSound(aux);
    }
    public void SetDeath(bool State)
    {
        _isDeath = State;
        _animator.SetBool("isDeath", _isDeath);
        _scriptMovement.SetMoveZero();
    }
    public Vector3 GetHipsPosition()
    {
        return _hipsPosition.position;
    }
    public Vector3 GetHeadPosition()
    {
        return _headReference.position;
    }
    public void SetAreaCatching(bool State)
    {
        _areaCatching.gameObject.SetActive(State);
    }
    private void GetStats()
    {
        _isCrouching = _scriptMovement.GetIsCrouching();
        _isMoving = _scriptMovement.GetIsMoving();
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
    public void SetIfPlayerCanMove(bool state)
    {
        _HasNoControl = state;
    }
    public void PlayerCanShoot(bool State)
    {
        _scriptController.PlayerCanShootAgain(State);
    }
    public Transform GetMegaphoneTransform()
    {
        return _megaphoneTransform;
    }
    public void ResetHeldSound()
    {
        _scriptShootingGun.ClearSound();
        _animator.SetBool("Grabbing", false); 
    }
    public bool PlayerHasSound()
    {
        return _scriptShootingGun.CheckSound();
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