using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerDash
{
    private Transform _modelTransform;
    private Vector3 _dir;
    private float _force=10.0f, _cooldown, _maxCooldown = 2f;
    private bool _canDash=true;
    private Rigidbody _rb;
    private PlayerManager _manager;
    private Animator _animator;
    private AudioClip _clip;
   private Image UIDashCooldown;
    public PlayerDash(Transform ModelTransform, Rigidbody rb, Animator animator)
    {
        UIDashCooldown = UIManager.Instance.CooldownCircleBar;
        _modelTransform=ModelTransform;
        _rb=rb;
        _animator=animator;
        _clip = AudioStorage.Instance.PlayerSound(EnumAudios.PlayerDash);
        _manager = GameManager.Instance.PlayerReference;
    }

    public void Dash()
    {
        if (!_canDash) return;
        _dir= _modelTransform.forward;
        _rb.useGravity=false;
        AudioManager.Instance.PlaySFX(_clip, 1.0f);
        _animator.SetTrigger("Dash");
        _rb.velocity = _dir * _force;
        _canDash=false; 
        _cooldown = _maxCooldown;
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
        UIRefresh();
    }

    private void UIRefresh()
    {
        if (_canDash == false)
        {
            float fill = 1f - (_cooldown / _maxCooldown);
            UIDashCooldown.fillAmount = fill;
        }
        else
        {
            UIDashCooldown.fillAmount = 1f;
            AudioStorage.Instance.LightSwitch();
        }
    }
}
