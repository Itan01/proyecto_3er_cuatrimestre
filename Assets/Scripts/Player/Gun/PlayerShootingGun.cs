using Autodesk.Fbx;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PlayerShootingGun
{
    private GameObject _soundReference;
    private float _speed=7.5f, _size=1.2f;
    private Transform _spawn, _orientation, _model;
    private Vector3 _direction, _aux;
    private bool _hasASound;
    private float _maxdistance = 500f;
    private LayerMask _layerMask;
    private RaycastHit _hitPoint;

    public PlayerShootingGun(Transform SpawnProyectil, Transform Orientation, Transform Model,LayerMask Mask)
    {
        _spawn = SpawnProyectil;
        _orientation=Orientation;
        _model=Model;
        _layerMask=Mask;
    }
     public void ThrowSound()
     {
        AvailableSound();
        _hasASound = false; 
        GameManager.Instance.UISound.Shooting();
     }

    public void SetSound(SoundStruct Sound)
    {
        _hasASound = true;
        _soundReference = Sound.Sound;
        GameManager.Instance.UISound.SetSound(Sound.Index);
    }
    public bool CheckSound()
    {
        return _hasASound;
    }

    private void AvailableSound() 
    {
        _hasASound = false;
        _model.forward = _direction;
        var NewSound = UnityEngine.Object.Instantiate(_soundReference, _spawn.position, Quaternion.identity);
        AbstractSound script = NewSound.GetComponent<AbstractSound>();
        script.SetTarget(null, 0.0f);
        script.SetDirection(_direction, _speed, _size);
        script.FreezeObject(false);
        script.PlayerCanCatchIt(false);
        script.SetIfPlayerSummoned(true);
        script.SetPlayerShootIt(true);
        _aux.y= 0.0f; 
        _model.transform.forward = _aux;
    }
    public void Direction()
    {
        Ray ray = new Ray(_orientation.position,_orientation.forward);

        if (Physics.Raycast(ray, out _hitPoint, _maxdistance, _layerMask))
        {
            _direction = (_hitPoint.point-_spawn.position).normalized;
            _aux = _direction;
        }
    }
}

