using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System.Security.Cryptography;

public class PlayerManager : EntityMonobehaviour
{
    private SetSizeCollider _scriptCollider;
    private PlayerController _scriptController;
    private PlayerMovement _scriptMovement;
    private PlayerShootingGun _scriptShootingGun;
    private PlayerInteractions _scriptInteractions;
    private GrabbingSound _areaCatching;

    [SerializeField] private bool _onCaptured = false;
    [Header("<color=green>LayersMask</color>")]
    [SerializeField] private LayerMask _soundMask;
    [SerializeField] private LayerMask _enviormentMask;
    [SerializeField] private LayerMask InteractMask;
    [SerializeField] private LayerMask _enviormentMaskWithOutPlayer;
    [Header("<color=red>Transform</color>")]
    [SerializeField] private Transform _camTransform;
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private Transform _spawnProyectil;
    [SerializeField] private Transform _hipsPosition;

    protected override void Awake()
    {
        GameManager.Instance.PlayerReference = this;
        GameManager.Instance.RespawnReference = transform.position;
    }
    protected override void Start()
    {
        _summonedByPlayer = true;
        _areaCatching = GetComponentInChildren<GrabbingSound>();
        SetAreaCatching(false);
        base.Start();
    }

    protected override void Update()
    {
        if (_isDeath) return;
        base.Update();
        CheckInputs();
        GetStats();
    }
    protected override void FixedUpdate()
    {
        _scriptMovement.Move();
    }
    private void CheckInputs()
    {
        _isMoving = _scriptController.CheckMovementInputs(_scriptMovement);
        _scriptController.CheckGunInputs();
        _scriptController.CheckInteractions();
    }
    protected override void GetScripts()
    {
        _scriptCollider = new SetSizeCollider(_capsuleCollider, _boxCollider);
        _scriptShootingGun = new PlayerShootingGun(_spawnProyectil, _camTransform,_modelTransform, _enviormentMaskWithOutPlayer);
        _scriptInteractions = new PlayerInteractions(transform, _camTransform, InteractMask);
        _scriptController = new PlayerController(_scriptShootingGun, _scriptInteractions, _animator);
        _scriptMovement = new PlayerMovement(transform, _rb, _camTransform, _modelTransform, _animator, _scriptCollider);
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

    public void SetAreaCatching(bool State)
    {
        _areaCatching.gameObject.SetActive(State);
    }
    private void GetStats()
    {
        _isCrouching = _scriptMovement.GetIsCrouching();
        _isMoving = _scriptMovement.GetIsMoving();
        _spawnProyectil.position = _hipsPosition.position + _camTransform.forward*1.5f;
    }
    public void SetCaptured(bool State)
    {
        _onCaptured = State;
    }
    public bool GetCaptured()
    {
        return _onCaptured;
    }
    public void PlayerCanShoot(bool State)
    {
        _scriptController.PlayerCanShootAgain(State);
    }
}