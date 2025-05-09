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

    public PlayerShootingGun(Transform SpawnProyectil, Transform Orientation)
    {
        _spawn = SpawnProyectil;
        _orientation=Orientation;
    }
     public void ThrowSound()
     {
        AbstractSound _script = _soundReference.GetComponent<AbstractSound>();
        _script.SetTarget(null, 0.0f);
        _script.PlayerCanCatchIt(false);
        _soundReference.transform.position = _spawn.position;
        _script.SetDirection(_spawn.position + _orientation.forward * 20, _speed, _size);
        _hasASound = false;
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
    public bool CheckSound()
    {
        return _hasASound;
    }
}

