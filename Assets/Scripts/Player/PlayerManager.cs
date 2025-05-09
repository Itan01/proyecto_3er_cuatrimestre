using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerController _scriptController;
    private PlayerMovement _scriptMovement;
    private PlayerShootingGun _scriptShootingGun;
    private PlayerGrabbingGun _scriptGrabbingGun;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _camTransform, _modelTransform, SpawnProyectil;
    private Rigidbody _rb;
    private Animator _animator;
    void Start()
    {
        GetComponents();
        GetScripts();
    }

    private void Update()
    {
    }
    private void FixedUpdate()
    {
        CheckInputs();

    }
    private void GetComponents()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void CheckInputs()
    {
        _scriptController.CheckMovementInputs(_scriptMovement);
        _scriptController.ChecKMouseInputs(_scriptGrabbingGun, _scriptShootingGun);
    }
    private void GetScripts()
    {
        _scriptController = new PlayerController();
        _scriptMovement = new PlayerMovement(transform, _rb, _camTransform, _modelTransform);
        _scriptGrabbingGun = new PlayerGrabbingGun(_scriptShootingGun, _modelTransform, _camTransform, _layerMask);
        _scriptShootingGun = new PlayerShootingGun(_scriptGrabbingGun);
    }
}
