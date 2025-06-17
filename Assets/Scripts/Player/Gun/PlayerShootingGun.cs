using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

public class PlayerShootingGun
{
    private GameObject _soundReference;
    private float _speed=7.5f, _size=1.2f;
    private Transform _spawn, _orientation, _player, _model;
    private bool _hasASound;

    public PlayerShootingGun(Transform SpawnProyectil, Transform Orientation, Transform Player, Transform Model)
    {
        _spawn = SpawnProyectil;
        _orientation=Orientation;
        _player=Player;
        _model=Model;
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
        Vector3 aux = _orientation.forward;
        _model.transform.forward = aux;
        _hasASound = false;
        var NewSound = UnityEngine.Object.Instantiate(_soundReference, _spawn.position, Quaternion.identity);
        AbstractSound script = NewSound.GetComponent<AbstractSound>();
        script.SetTarget(null, 0.0f);
        script.SetDirection(aux, _speed, _size);
        script.FreezeObject(false);
        script.PlayerCanCatchIt(false);
        script.SetIfPlayerSummoned(true);
        script.SetPlayerShootIt(true);
        script.SetSpawnPoint(_player.position);
        aux.y = 0.0f;
        _model.transform.forward = aux;
    }
}

