using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    private PlayerController _scriptController;
    private PlayerMovement _scriptMovement;
    private PlayerShootingGun _scriptShootingGun;
    private PlayerGrabbingGun _scriptGrabbingGun;
    private PlayerAnimation _scriptAnimation;
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
        _animator = GetComponentInChildren<Animator>();
    }

    private void CheckInputs()
    {
        _isMoving=_scriptController.CheckMovementInputs(_scriptMovement);
        _scriptController.ChecKGrabGunInput(_scriptGrabbingGun);
        _scriptController.ChecKShootGunInput(_scriptShootingGun);
    }
    private void GetScripts()
    {
        _scriptAnimation = new PlayerAnimation(_animator);
        _scriptController = new PlayerController();
        _scriptMovement = new PlayerMovement(transform, _rb, _camTransform, _modelTransform, _scriptAnimation);
        _scriptGrabbingGun = new PlayerGrabbingGun(_modelTransform, _camTransform, _soundMask,_enviormentMask);
        _scriptShootingGun = new PlayerShootingGun(_spawnProyectil, _camTransform);
    }

    public void ShootGunSetSound(GameObject reference)
    {
        _scriptShootingGun.SetSound(reference);
    }

}
