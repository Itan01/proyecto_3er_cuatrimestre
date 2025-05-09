using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerGrabbingGun
{
    public bool _canCatch = false;
    private PlayerShootingGun _scriptShoot;
    private AbstractSound _scriptSound;
    private float _timer = 0.0f, _timerRef = 1.0f;
    private RaycastHit _CatchHit;
    private Vector3 _sizeBox;
    private float _length = 1.0f, _CatchingDistance=20.0f;
    private Transform _transform, _orientation;

    public PlayerGrabbingGun(PlayerShootingGun ScriptShoot, Transform PlayerTransform, Transform Orientation,LayerMask LayerMask)
    {
        _scriptShoot = ScriptShoot;
        _transform = PlayerTransform;
        _sizeBox = new Vector3(_length, _length, _length);
        _orientation = Orientation;
    }
    public void CatchingSound(){
        Vector3 _startPoint = _transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        if (Physics.BoxCast(_startPoint, _sizeBox, _orientation.forward, out _CatchHit, Quaternion.identity, _CatchingDistance))
        {
            Debug.Log($"Collided obj : {_CatchHit.collider.name}.");

            if (_CatchHit.collider.TryGetComponent(out AbstractSound Sound))
            {
                Sound.SetTarget(_transform, 20.0f);
            }
        }
    }
    private void SetTimer(){
        _canCatch = false;
        _timer = _timerRef;
    }
    private void SubstractTimer()
    {
        if(_timer > 0)
        _timer -= 1 * Time.deltaTime;
        else
        {
            _timer = 0;
            _canCatch = true;
        }
    }

}
