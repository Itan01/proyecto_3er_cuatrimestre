using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

public class PlayerShootingGun
{
    private GameObject _soundReference;
    private float _speed=6.0f, _size=1.2f;
    private Transform _spawn, _orientation, _player;
    [SerializeField] private bool _soundEnabled = true;
    private bool _hasASound;
    private UISetSound _scriptUISound;

    public PlayerShootingGun(Transform SpawnProyectil, Transform Orientation, UISetSound UI, Transform Player)
    {
        _spawn = SpawnProyectil;
        _orientation=Orientation;
        _scriptUISound = UI;
        _player=Player;
    }
     public void ThrowSound()
     {
        AvailableSound();
        _scriptUISound.SetSound(0);
     }

    public void SetSound(GameObject Sound)
    {
        if(_soundReference != null)
          _soundReference.GetComponent<AbstractSound>().FreezeObject(false);
        _hasASound = true;
        Vector3 _auxVector = new Vector3(0.0f, 10000.0f, 0.0f);
        _soundReference = UnityEngine.Object.Instantiate(Sound, _auxVector, Quaternion.identity);
        AbstractSound script= _soundReference.GetComponent<AbstractSound>();
        script.FreezeObject(true);
    }

    public void ShootEnable(bool state) 
    { 
        _soundEnabled = state;
    }
    public bool CheckSound()
    {
        return _hasASound;
    }

    private void AvailableSound() 
    {
        _hasASound = false;
        var NewSound = UnityEngine.Object.Instantiate(_soundReference, _spawn.position, Quaternion.identity);
        AbstractSound script = NewSound.GetComponent<AbstractSound>();
        script.SetTarget(null, 0.0f);
        script.SetDirection(_orientation.forward, _speed, _size);
        script.FreezeObject(false);
        script.PlayerCanCatchIt(false);
        script.SetIfPlayerSummoned(true);
        script.SetSpawnPoint(_player.position);
    }

}

