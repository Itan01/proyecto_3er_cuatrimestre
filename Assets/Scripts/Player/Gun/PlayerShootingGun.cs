using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerShootingGun
{
    private GameObject _soundReference;
    private PlayerGrabbingGun _scriptGrab;
    private float _speed, _size;
    [SerializeField] private bool _soundEnabled = true;
    private bool _hasASound;

    public PlayerShootingGun(PlayerGrabbingGun ScriptGrab)
    {
        _scriptGrab= ScriptGrab;
    }
    private void Update()
     {
         if (_hasASound && Input.GetMouseButtonDown(0) && _soundEnabled)
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
        //var ThrowingSound = Instantiate(_soundShoot[_indexBullet], _spawnProyectil.position, Quaternion.identity);

        //_scriptSound = ThrowingSound.GetComponent<AbsStandardSoundMov>();
        //_scriptSound.SetDirection(_orientationProyectil.position, 10.0f, _size);

        //var impactExplosion = ThrowingSound.GetComponent<ImpactExplosion>();
        //if (impactExplosion != null)
        //{
        //    impactExplosion.MarkAsThrown();
        //}

        //_typeOfSound.sprite = _sprites[4];
        //_hasASound = false;
        //_scriptGrab.CheckSound(false);
     }

    public void SetSound(GameObject Sound)
    {
        _hasASound = true;
        Vector3 _auxVector = new Vector3(0.0f, 10000.0f, 0.0f);
        _soundReference = UnityEngine.Object.Instantiate(Sound, _auxVector, Quaternion.identity);
        Debug.Log("SonidoCreado");
    }

    public void ShootEnable(bool state) 
    { 
        _soundEnabled = state;
    }
}

