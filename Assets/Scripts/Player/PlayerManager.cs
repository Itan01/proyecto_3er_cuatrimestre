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
    [Header("<color=green>LayersMask</color>")]
    [SerializeField] private LayerMask _soundMask;
    [SerializeField] private LayerMask _enviormentMask;
    [SerializeField] private LayerMask InteractMask;
    [Header("<color=red>Transform</color>")]
    [SerializeField] private Transform _camTransform;
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private Transform _spawnProyectil;
    [Header("<color=blue>UI</color>")]
    [SerializeField] private TMP_Text _pointsUI;
    [Header("<color=yellow>Variables and Prefabs</color>")]
    [SerializeField] private bool _isMoving = false;
    [SerializeField] GameObject _areaCatching;
    [SerializeField] UISetSound _scriptUISound;
    protected override void Start()
    {
        base.Start();
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
    private void CheckInputs()
    {
        _isMoving=_scriptController.CheckMovementInputs(_scriptMovement);
        _scriptController.CheckGrabGunInput(_scriptGrabbingGun);
        _scriptController.CheckShootGunInput(_scriptShootingGun);
        _scriptController.CheckInteractions(_scriptInteractions);
    }
    protected override void GetScripts()
    {
        _scriptAnimation = new PlayerAnimation(_animator);
        _scriptScore = new PlayerScore(_pointsUI);
        _scriptController = new PlayerController();
        _scriptMovement = new PlayerMovement(transform, _rb, _camTransform, _modelTransform, _scriptAnimation);
        _scriptGrabbingGun = new PlayerGrabbingGun(_modelTransform, _camTransform, _soundMask,_enviormentMask, _areaCatching);
        _scriptShootingGun = new PlayerShootingGun(_spawnProyectil, _camTransform, _scriptUISound);
        _scriptInteractions = new PlayerInteractions(_scriptScore, transform, _camTransform, InteractMask);

    }

    public void ShootGunSetSound(GameObject reference)
    {
        _scriptShootingGun.SetSound(reference);
    }
    public void SetSoundUI(int index)
    {
        _scriptUISound.SetSound(index);
    }

}
