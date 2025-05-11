using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerGrabbingGun
{
    private GameObject _areaCatching;
    private RaycastHit _CatchHit;
    private Vector3 _sizeBox, _startPoint;
    private float _length = 3.0f, _CatchingDistance=20.0f;
    private Transform _transform, _orientation;
    private LayerMask _soundMask, _enviromentMask;

    public PlayerGrabbingGun(Transform PlayerTransform, Transform Orientation, LayerMask SoundMask, LayerMask EnviromentMask, GameObject AreaCatching)
    {
        _transform = PlayerTransform;
        _sizeBox = new Vector3(_length, _length, _length);
        _orientation = Orientation;
        _soundMask = SoundMask;
        _enviromentMask=EnviromentMask;
        _areaCatching= AreaCatching;
    }
    public void CatchingSound()
    {
        _startPoint = _transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        _areaCatching.SetActive(true);
        _areaCatching.transform.forward=_orientation.forward;
        if (Physics.BoxCast(_startPoint, _sizeBox, _orientation.forward * _CatchingDistance, out _CatchHit, Quaternion.identity, _CatchingDistance, _soundMask))
        {
            if (_CatchHit.collider.TryGetComponent(out AbstractSound Sound))
            {
                if(Sound.HasLineOfVision(_enviromentMask, _startPoint))
                {
                    Sound.PlayerCanCatchIt(true);
                    Sound.SetTarget(_transform, 20.0f);
                }
                    
            }
        }
    }
    public void StopCatching()
    {
        _areaCatching.SetActive(false);
    }
}
