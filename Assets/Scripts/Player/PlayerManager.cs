using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class PlayerManager : EntityMonobehaviour
{
    private SetSizeCollider _scriptCollider;
    private PlayerController _scriptController;
    private PlayerMovement _scriptMovement;
    private PlayerShootingGun _scriptShootingGun;
    private PlayerGrabbingGun _scriptGrabbingGun;
    private PlayerInteractions _scriptInteractions;
    private float _counter = 0;
    [Header("<color=green>LayersMask</color>")]
    [SerializeField] private LayerMask _soundMask;
    [SerializeField] private LayerMask _enviormentMask;
    [SerializeField] private LayerMask InteractMask;
    [Header("<color=red>Transform</color>")]
    [SerializeField] private Transform _camTransform;
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private Transform _spawnProyectil;
    [SerializeField] private Transform _hipsPosition;
    [Header("<color=yellow>Variables and Prefabs</color>")]
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private bool _isDeath =false;
    [SerializeField] private bool _isCrouching=false;
    [SerializeField] GameObject _areaCatching;
    [SerializeField] UISetSound _scriptUISound;
    [SerializeField] TransitionFade _scriptTransition;

    protected override void Awake()
    {
        GameManager.Instance.PlayerReference = this;
        GameManager.Instance.RespawnReference=transform.position;
    }
    protected override void Start()
    {

        base.Start();
    }

    protected override void Update()
    {
        if (!_isDeath)
            CheckInputs();
        else
            AddCounter();
        GetStats();
    }
    protected override void FixedUpdate()
    {
        if (_isMoving)
            _scriptMovement.Move();
    }
    private void CheckInputs()
    {
        _isMoving=_scriptController.CheckMovementInputs(_scriptMovement);
        _scriptController.CheckGunInputs();
        _scriptController.CheckInteractions();
    }
    protected override void GetScripts()
    {
        _scriptCollider = new SetSizeCollider(_capsuleCollider,_boxCollider);
        _scriptGrabbingGun = new PlayerGrabbingGun(_modelTransform, _camTransform, _soundMask, _enviormentMask, _areaCatching);
        _scriptShootingGun = new PlayerShootingGun(_spawnProyectil, _camTransform, _scriptUISound);
        _scriptInteractions = new PlayerInteractions(transform, _camTransform, InteractMask);
        _scriptController = new PlayerController(_scriptGrabbingGun, _scriptShootingGun, _scriptInteractions, _animator);
        _scriptMovement = new PlayerMovement(transform, _rb, _camTransform, _modelTransform, _animator, _scriptCollider);
    }

    public void ShootGunSetSound(GameObject reference)
    {
        _scriptShootingGun.SetSound(reference);
    }
    public void SetSoundUI(int index)
    {
        _scriptUISound.SetSound(index);
    }
    public void SetDeathAnimation()
    {
        _isDeath=true;
        _animator.SetBool("isDeath", _isDeath);
        _scriptMovement.SetMoveZero();
        _scriptTransition.ShowBlackScreen();
    }
    private void AddCounter()
    {
        _counter += Time.deltaTime;
        if ( _counter > 1.75f)
        {
            _counter=0;
            _isDeath = false;
            _animator.SetBool("isDeath",_isDeath);
            _scriptTransition.FadeOut();
            transform.position = GameManager.Instance.RespawnReference;
        }
    }

    public Transform GetHipsPosition()
    {
        return _hipsPosition;
    }

    public void SetAreaCatching(bool State)
    {
       _areaCatching.SetActive(State);
    }

    public bool IsPlayerMoving()
    {
        return _isMoving;
    }
    public bool IsPlayerCrouching()
    {
        return _isCrouching;
    }
    public bool IsPlayerDeath()
    {
        return _isDeath;
    }
    private void GetStats()
    {
        _isCrouching=_scriptMovement.GetIsCrouching();
        _isMoving=_scriptMovement.GetIsMoving();
    }
}
