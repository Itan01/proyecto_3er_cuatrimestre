using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController
{
    private PlayerShootingGun _scriptShoot;
    private PlayerInteractions _scriptInteract;
    private PlayerMovement _scriptMovement;
    private bool _canShoot = false;
    private Animator _animator;
    private GrabbingSound _area;
    private PlayerInputReader _input;
    private PlayerDash _scriptDash;
    public event Action CheckInputs;
    private float _x, _z;
    private bool  _crouch;

    private bool _grabbing = false,_aiming=false;
    public PlayerController(PlayerShootingGun scriptShoot, PlayerInteractions scriptInteract, PlayerDash scriptDash,PlayerMovement scriptMovement, Animator animator, GrabbingSound area)
    {
        _scriptShoot = scriptShoot;
        _scriptInteract = scriptInteract;
        _scriptDash= scriptDash;
        _scriptMovement= scriptMovement;
        _animator = animator;
        _area = area;
        CheckInputs += Movement;
        CheckInputs += Grabbing;
        CheckInputs += Interactions;
        CheckInputs += Dash;
        CheckInputs += Shoot;
    }
    public void Inputs()
    {
        CheckInputs?.Invoke();
    }
    public void Movement()
    {
        _x = Input.GetAxisRaw("Horizontal");
        _z = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
            _crouch = !_crouch;
        _scriptMovement.InputsMoving(_x, _z, _crouch);
    }

    public void Aiming()
    {
        _scriptShoot.Direction();
        if (Input.GetKeyUp(KeyCode.Mouse0))
            SetGrabing(true);
        else if (Input.GetKeyDown(KeyCode.Mouse1))
            SetGrabing(false);
    }

    public void Shoot()
    {
        _canShoot = _scriptShoot.CheckSound();
        if (!_canShoot) return;
        _aiming = Input.GetKeyDown(KeyCode.Mouse0);
        if (_aiming)
        {
            GameManager.Instance.CameraReference.GetComponent<CameraManager>().SetCameraDistance(2.0f);
            UIManager.Instance.AimUI.UITrigger(_aiming);
            _animator.SetBool("StartShooting", _aiming);
            CheckInputs += Aiming;
            CheckInputs -=Shoot;
            CheckInputs -= Grabbing;
        }
    }

    public void Interactions()
    {
        if (Input.GetKey(KeyCode.E))
        {
            _scriptInteract.Interact();
        }
    }

    public void Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _scriptDash.Dash();
        }
    }

    public void Grabbing()
    {
        bool isRightClick = Input.GetKey(KeyCode.Mouse1);

        if (isRightClick)
        {
            // Activar de inmediato
            if (!_grabbing)
            {
                _animator.SetBool("Grabbing", true);
            }
        }
        else
        {
            if (_grabbing)
            {
                _animator.SetBool("Grabbing", false);
                _area.Desactivate();
            }
        }

        _grabbing = isRightClick;
    }

    public void PlayerCanShootAgain(bool state)
    {
        _canShoot = state;
    }

    private void SetGrabing(bool state)
    {
        if (state)
        {
            _animator.SetTrigger("Shooting");
            _scriptShoot.ThrowSound();
        }
        CheckInputs -= Aiming;
        CheckInputs += Grabbing;
        CheckInputs += Shoot;
        _animator.SetBool("StartShooting", false);
        GameManager.Instance.CameraReference.GetComponent<CameraManager>().ResetCameraDistance();
        UIManager.Instance.AimUI.UITrigger(false);
    }
}
