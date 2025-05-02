using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShootingGun))]

public class GrabbingGun : AbsSettingGun
{
    public bool _canCatch = false;
    private ShootingGun _scriptShoot;
    private AbsStandardSoundMov _scriptSound;
    [SerializeField] private GameObject _AreaCathing;

    protected override void Start()
    {
        _timer = _timerRef;
        _scriptShoot = GetComponent<ShootingGun>();
    }
    protected override void Update()
    {
        if (!_canCatch)
            SubstractTimer();
        if (_canCatch && Input.GetMouseButtonDown(1))
            CatchingSound();
    }

    private void CatchingSound(){
        _canCatch = true;
        var _Area = Instantiate(_AreaCathing, _spawnProyectil.position, Quaternion.identity, _spawnProyectil);
        _Area.transform.LookAt(_orientationProyectil);
        SetTimer();
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
