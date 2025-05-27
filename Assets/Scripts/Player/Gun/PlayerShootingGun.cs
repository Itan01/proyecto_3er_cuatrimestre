using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

public class PlayerShootingGun
{
    private GameObject _soundReference;
    private float _speed=5.0f, _size=1.0f;
    private Transform _spawn, _orientation;
    [SerializeField] private bool _soundEnabled = true;
    private bool _hasASound;
    private UISetSound _scriptUISound;

    public PlayerShootingGun(Transform SpawnProyectil, Transform Orientation, UISetSound UI)
    {
        _spawn = SpawnProyectil;
        _orientation=Orientation;
        _scriptUISound = UI;
    }
     public void ThrowSound()
     {
        AvailableSound();
        _scriptUISound.SetSound(0);
        _soundReference.transform.position = _spawn.position;
        _hasASound = false;
     }

    public void SetSound(GameObject Sound)
    {
        _hasASound = true;
        Vector3 _auxVector = new Vector3(0.0f, 10000.0f, 0.0f);
        var Reference=_soundReference = UnityEngine.Object.Instantiate(Sound, _auxVector, Quaternion.identity);
        AbstractSound script= Reference.GetComponent<AbstractSound>();
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
        AbstractSound script = _soundReference.GetComponent<AbstractSound>();
        script.SetTarget(null,0.0f);
        script.SetDirection(_orientation.forward, _speed, _size);
        script.FreezeObject(false);
        script.PlayerCanCatchIt(false);
        script.SetIfPlayerSummoned(true);
        script.SetSpawnPoint(_spawn.position);
    }

}

