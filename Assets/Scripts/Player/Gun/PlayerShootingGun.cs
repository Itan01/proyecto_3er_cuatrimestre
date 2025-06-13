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
    private bool _soundEnabled = true;
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
        SetSound(GameManager.Instance.SoundsReferences.GetSoundComponents(0));
     }

    public void SetSound(SoundStruct Sound)
    {
        if(Sound.Index != 0)
        {
            _hasASound = true;
            _soundReference = Sound.Sound;
        }
        else
        {
            _hasASound=false;
        }
        GameManager.Instance.UISound.SetSound(Sound.SpriteUi, Sound.Index);
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

