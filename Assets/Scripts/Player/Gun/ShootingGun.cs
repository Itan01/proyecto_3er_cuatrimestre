using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShootingGun : AbsSettingGun
{
    [SerializeField] private GameObject[] _soundShoot;
    [SerializeField] private int _indexBullet;
    [SerializeField] private Image _typeOfSound;
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
            _indexBullet = 1;
        }
     }
     private void ThrowSound()
     {
         var ThrowingSound = Instantiate(_soundShoot[_indexBullet], _spawnProyectil.position, Quaternion.identity);
         _scriptSound = ThrowingSound.GetComponent<AbsStandardSoundMov>();
         _scriptSound.SetDirection(_orientationProyectil.position, 10.0f, _size);
        _typeOfSound.color = Color.white;
         _hasASound = false;

     }
    public void SetSound(int Index, float Speed, float Size)
    {
        _indexBullet = Index;
        _speed = Speed;
        _size = Size;
        _typeOfSound.color = _indexBullet==0 ? Color.green : _indexBullet == 1 ? Color.blue : Color.magenta;
    }
}
