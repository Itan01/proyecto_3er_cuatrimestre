using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGun : AbsSettingGun
{
     [SerializeField] private GameObject _soundShoot;
     private AbsStandardSoundMov _scriptSound;
    private GrabbingGun _scriptShoot;
    private float _speed, _size;

     protected override void Start()
     {
        _scriptShoot = GetComponent<GrabbingGun>();
    }

    protected override void Update()
     {
         if (_hasASound && Input.GetMouseButtonDown(0))
         {
             ThrowSound();
         }
         if (Input.GetKeyDown(KeyCode.T))
         {
             _hasASound = true;
         }
     }
     private void ThrowSound()
     {
         var ThrowingSound = Instantiate(_soundShoot, _spawnProyectil.position, Quaternion.identity);
         _scriptSound = ThrowingSound.GetComponent<AbsStandardSoundMov>();
         _scriptSound.Spawn(_spawnProyectil, _orientationProyectil, _speed, _size);
         _hasASound = false;

     }
    public void SetSound(GameObject Object, float Speed, float Size)
    {
        _soundShoot = Object;
        _speed = Speed;
        _size = Size;
    }
}
