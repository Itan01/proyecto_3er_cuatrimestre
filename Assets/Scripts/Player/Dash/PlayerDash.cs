using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash
{
    private Transform _modelTransform;
    private Vector3 _dir;
    private float _force=10.0f, _cooldown;
    private bool _canDash=true;
    private Rigidbody _rb;
    private PlayerManager _manager;
    private Animator _animator;
    public PlayerDash(Transform ModelTransform, Rigidbody rb, Animator animator)
    {
        _modelTransform=ModelTransform;
        _rb=rb;
        _animator=animator;
        _manager = GameManager.Instance.PlayerReference;
    }

    public void Dash()
    {
        if (!_canDash) return;
        _dir= _modelTransform.forward;
        _rb.useGravity=false;
        AudioStorage.Instance.Dash();
        _animator.SetTrigger("Dash");
        _rb.velocity = _dir * _force;
        _canDash=false; 
        _cooldown = 2.0f;
        _manager.SubtractTimer += Cooldown;

    }
    public void Cooldown()
    {
        _cooldown-= Time.deltaTime;
        if (_cooldown < 0) 
        {
            _canDash = true;
            _manager.SubtractTimer -= Cooldown;
            _cooldown = 0.0f;
        }

    }
}
