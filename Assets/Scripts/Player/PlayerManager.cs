using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System.Security.Cryptography;
using UnityEditor;

public class PlayerManager : EntityMonobehaviour
{
    private SetSizeCollider _scriptCollider;
    private PlayerController _scriptController;
    private PlayerMovement _scriptMovement;
    private PlayerShootingGun _scriptShootingGun;
    private PlayerInteractions _scriptInteractions;
    private GrabbingSound _areaCatching;
    private PlayerInputReader _inputReader;

    [SerializeField] private bool _onCaptured = false;
    [Header("<color=green>LayersMask</color>")]
    [SerializeField] private LayerMask _soundMask;
    [SerializeField] private LayerMask _enviormentMask;
    [SerializeField] private LayerMask _interactMask;
    [SerializeField] private LayerMask _enviormentMaskWithOutPlayer;
    [Header("<color=red>Transform</color>")]
    [SerializeField] private Transform _camTransform;
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private Transform _spawnProyectil;
    [SerializeField] private Transform _hipsPosition;
    [SerializeField] private Transform _megaphoneTransform;

    protected override void Awake()
    {
        GameManager.Instance.PlayerReference = this;
        GameManager.Instance.RespawnReference = transform.position;
        _inputReader = GetComponent<PlayerInputReader>();
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
        if (UIManager.Instance.IsMenuActive()) return; // bloquea inputs si el men� est� activo

        _isMoving = _scriptController.CheckMovementInputs(_scriptMovement);
        _scriptController.CheckGunInputs();
        _scriptController.CheckInteractions();
    }
    protected override void GetScripts()
    {
        _scriptCollider = new SetSizeCollider(_capsuleCollider, _boxCollider);
        _scriptShootingGun = new PlayerShootingGun(_megaphoneTransform, _camTransform, _modelTransform, _enviormentMaskWithOutPlayer);
        _scriptInteractions = new PlayerInteractions(_interactMask);
        _scriptController = new PlayerController(_scriptShootingGun, _scriptInteractions, _animator, _areaCatching, _inputReader);
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

}