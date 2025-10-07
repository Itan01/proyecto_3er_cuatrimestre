using System;
using UnityEngine;
public class PlayerController
{
    private Control_Shoot _scriptShoot;
    private Control_Interact _scriptInteract;
    private Control_Mov _scriptMovement;
    private bool _canShoot = false;
    private Animator _animator;
    private Control_Gun _area;
    private PlayerInputReader _input;
    private Control_Dash _scriptDash;
    public event Action CheckInputs;
    private float _x, _z;
    private bool  _crouch;
    private bool _canUseGun = true;

    private bool _grabbing = false,_aiming=false;
    public PlayerController()
    {
    }
    public void Inputs()
    {
        CheckInputs?.Invoke();
    }
    //public void Movement()
    //{
    //    _x = Input.GetAxisRaw("Horizontal");
    //    _z = Input.GetAxisRaw("Vertical");
    //    if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
    //        _crouch = !_crouch;
    //    _scriptMovement.InputsMoving(_x, _z, _crouch);
    //}

    //public void Aiming()
    //{
    //    _scriptShoot.Direction();
    //    if (Input.GetKeyUp(KeyCode.Mouse0))
    //        SetGrabing(true);
    //    else if (Input.GetKeyDown(KeyCode.Mouse1))
    //        SetGrabing(false);
    //}

    //public void Shoot()
    //{
    //    if (!_canUseGun) return;
    //    _canShoot = _scriptShoot.CheckSound();
    //    if (!_canShoot || _grabbing) return;
    //    _aiming = Input.GetKeyDown(KeyCode.Mouse0);
    //    if (_aiming)
    //    {
    //        GameManager.Instance.CameraReference.GetComponent<CameraManager>().SetCameraDistance(2.0f);
    //        UIManager.Instance.AimUI.UITrigger(_aiming);
    //        _animator.SetBool("StartShooting", _aiming);
    //        CheckInputs += Aiming;
    //        CheckInputs -=Shoot;
    //        CheckInputs -= Grabbing;
    //    }
    //}


    //public void Grabbing()
    //{
    //    if (!_canUseGun) return;
    //    bool isRightClick = Input.GetKey(KeyCode.Mouse1);
    //    if (!isRightClick)
    //    {
    //        _area.Desactivate();
    //    }
    //    _animator.SetBool("Grabbing", isRightClick);
    //    _grabbing = isRightClick;
    //}

    //public void PlayerCanShootAgain(bool state)
    //{
    //    _canShoot = state;
    //}

    //private void SetGrabing(bool state)
    //{
    //    if (state)
    //    {
    //        _animator.SetTrigger("Shooting");
    //        _scriptShoot.ThrowSound();
    //    }
    //    CheckInputs -= Aiming;
    //    CheckInputs += Grabbing;
    //    CheckInputs += Shoot;
    //    _animator.SetBool("StartShooting", false);
    //    GameManager.Instance.CameraReference.GetComponent<CameraManager>().ResetCameraDistance();
    //    UIManager.Instance.AimUI.UITrigger(false);
    //}
    //public void CanUseGun(bool State)
    //{
    //    _canUseGun = State;
    //}
}
