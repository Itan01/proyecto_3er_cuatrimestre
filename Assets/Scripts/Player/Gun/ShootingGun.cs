using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShootingGun : AbsSettingGun
{
    [SerializeField] private GameObject[] _soundShoot;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private int _indexBullet=0;
    [SerializeField] private Image _typeOfSound;
    private AbsStandardSoundMov _scriptSound;
    private GrabbingGun _scriptGrab;
    private float _speed, _size;
    [SerializeField] private bool _soundEnabled = true;

     protected override void Start()
     {
        _scriptGrab = GetComponent<GrabbingGun>();
    }

    protected override void Update()
     {
         if (_hasASound && Input.GetMouseButtonDown(0) && _soundEnabled)
         {
             ThrowSound();
         }
         if (Input.GetKeyDown(KeyCode.T))
         {
            SetSound(_indexBullet, 5.0f, 1.0f);
             _hasASound = true;
        }
     }
     private void ThrowSound()
     {
        var ThrowingSound = Instantiate(_soundShoot[_indexBullet], _spawnProyectil.position, Quaternion.identity);

        _scriptSound = ThrowingSound.GetComponent<AbsStandardSoundMov>();
        _scriptSound.SetDirection(_orientationProyectil.position, 10.0f, _size);

        var impactExplosion = ThrowingSound.GetComponent<ImpactExplosion>();
        if (impactExplosion != null)
        {
            impactExplosion.MarkAsThrown();
        }

        _typeOfSound.sprite = _sprites[4];
        _hasASound = false;
        _scriptGrab.CheckSound(false);
     }

    public void SetSound(int Index, float Speed, float Size)
    {
        _indexBullet = Index;
        _speed = Speed;
        _size = Size;
        _typeOfSound.sprite= _sprites[Index];
    }

    public void ShootEnable(bool state) 
    { 
        _soundEnabled = state;
    }
}

