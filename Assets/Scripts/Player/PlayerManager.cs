using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerController _scriptController;
    private PlayerMovement _scriptMovement;
    private PlayerShootingGun _scriptShootingGun;
    private PlayerGrabbingGun _scriptGrabbingGun;
    [SerializeField] private LayerMask _soundMask, _enviormentMask;
    [SerializeField] private Transform _camTransform, _modelTransform, _spawnProyectil;
    private Rigidbody _rb;
    private Animator _animator;
    [SerializeField]private bool _isMoving=false;
    void Start()
    {
        GetComponents();
        GetScripts();
    }

    private void Update()
    {
        CheckInputs();
    }
    private void FixedUpdate()
    {
        if (_isMoving)
            _scriptMovement.Move();
    }
    private void GetComponents()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _animator = GetComponent<Animator>();
    }

    private void CheckInputs()
    {
        _isMoving=_scriptController.CheckMovementInputs(_scriptMovement);
        _scriptController.ChecKGrabGunInput(_scriptGrabbingGun);
        _scriptController.ChecKShootGunInput(_scriptShootingGun);
    }
    private void GetScripts()
    {
        _scriptController = new PlayerController();
        _scriptMovement = new PlayerMovement(transform, _rb, _camTransform, _modelTransform);
        _scriptGrabbingGun = new PlayerGrabbingGun(_scriptShootingGun, _modelTransform, _camTransform, _soundMask);
        _scriptShootingGun = new PlayerShootingGun(_scriptGrabbingGun, _spawnProyectil);
    }

    public void ShootGunSetSound(GameObject reference)
    {
        _scriptShootingGun.SetSound(reference);
    }
}
