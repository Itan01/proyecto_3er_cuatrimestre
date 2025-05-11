using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class PlayerManager : EntityMonobehaviour
{
    private PlayerController _scriptController;
    private PlayerMovement _scriptMovement;
    private PlayerShootingGun _scriptShootingGun;
    private PlayerGrabbingGun _scriptGrabbingGun;
    private PlayerAnimation _scriptAnimation;
    private PlayerInteractions _scriptInteractions;
    private PlayerScore _scriptScore;
    [SerializeField] private LayerMask _soundMask, _enviormentMask, InteractMask;
    [SerializeField] private Transform _camTransform, _modelTransform, _spawnProyectil;
    [SerializeField] private bool _isMoving=false;
    [SerializeField] private TMP_Text _pointsUI;
    protected override void Start()
    {
        GetComponents();
        GetScripts();
    }

    protected override void Update()
    {
        CheckInputs();
    }
    protected override void FixedUpdate()
    {
        if (_isMoving)
            _scriptMovement.Move();
    }
    private void GetComponents()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _animator = GetComponentInChildren<Animator>();
    }

    private void CheckInputs()
    {
        _isMoving=_scriptController.CheckMovementInputs(_scriptMovement);
        _scriptController.CheckGrabGunInput(_scriptGrabbingGun);
        _scriptController.CheckShootGunInput(_scriptShootingGun);
        _scriptController.CheckInteractions(_scriptInteractions);
    }
    private void GetScripts()
    {
        _scriptAnimation = new PlayerAnimation(_animator);
        _scriptScore = new PlayerScore(_pointsUI);
        _scriptController = new PlayerController();
        _scriptMovement = new PlayerMovement(transform, _rb, _camTransform, _modelTransform, _scriptAnimation);
        _scriptGrabbingGun = new PlayerGrabbingGun(_modelTransform, _camTransform, _soundMask,_enviormentMask);
        _scriptShootingGun = new PlayerShootingGun(_spawnProyectil, _camTransform);
        _scriptInteractions = new PlayerInteractions(_scriptScore, transform, _camTransform, InteractMask);

    }

    public void ShootGunSetSound(GameObject reference)
    {
        _scriptShootingGun.SetSound(reference);
    }

}
