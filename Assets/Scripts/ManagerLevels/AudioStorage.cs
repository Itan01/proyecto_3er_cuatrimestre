using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStorage : MonoBehaviour
{
    /*Son getters & setters, seria como una biblioteca
    los otros scripts llaman a este para obtener el audio respecitvo

    AudioStorage.Instance.(nombre del Getter&Setter)



     */
    //Nuevos
    [Header("<color=green>News</color>")]
    [SerializeField] private AudioSource _objFragPlayerNear;
    [SerializeField] private AudioSource _objValuablePlayerNear;
    [SerializeField] private AudioSource _destroyValuableObj;
    [SerializeField] private AudioSource _destoyObjFrag;
    [SerializeField] private AudioSource _grabObj;
    [SerializeField] private AudioSource _timer;
    [SerializeField] private AudioSource _fuseBoxGeneratesSounds;
    [SerializeField] private AudioSource _stunnedEnemy;
    [SerializeField] private AudioSource _walkEnemy;
    [SerializeField] private AudioSource _walkRoomba;
    [SerializeField] private AudioSource _uiGun;
    [SerializeField] private AudioSource _soundPath;
    [SerializeField] private AudioSource _cadenzaDead;
    [SerializeField] private AudioSource _spawnRoomba;
    //
    [Header("<color=green>Rooms</color>")]

    [SerializeField] private AudioClip _glassSound, _smokeTrapSound, _laserAlarm, _zapSound;

    [Header("<color=green>Player Sounds</color>")]

    [SerializeField] private AudioClip _Dash;
    [SerializeField] private AudioClip _walking;
    Dictionary<EnumAudios, AudioClip> _player = new Dictionary<EnumAudios, AudioClip>();

    [Header("<color=green>PlayerGun Sounds</color>")]
    [SerializeField] private AudioClip _shootingSound;
    [SerializeField] private AudioClip _grabbingSound;
    Dictionary<EnumAudios, AudioClip> _gun = new Dictionary<EnumAudios, AudioClip>();

    [Header("<color=green>Lights Sound </color>")]
    [SerializeField] private AudioClip _LightSwitch;
    [Header("<color=green>Roomba Sounds</color>")]
    [SerializeField] private AudioClip _crashSound;
    [SerializeField] private AudioClip _Roombaexplosion;

    Dictionary<EnumAudios, AudioClip> _soundsGameObjects = new Dictionary<EnumAudios, AudioClip>();
    [Header("<color=green>Enemy Sounds</color>")]
    [SerializeField] private AudioClip _enemyConfused;
    [SerializeField] private AudioClip _enemyAlert;
    [SerializeField] private AudioClip _enemyHmm;
    [SerializeField] private AudioClip _enemyHurt;

    Dictionary<EnumAudios, AudioClip> _standardEnemy = new Dictionary<EnumAudios, AudioClip>();
    [Header("<color=green>Door Sounds</color>")]
    [SerializeField] private AudioClip _openDoorSound;
    [SerializeField] private AudioClip _closeDoorSound;
    [Header("<color=green>Camera Sounds</color>")]

    [SerializeField] private AudioClip _cameraDetection;
    [SerializeField] private AudioClip _cameraResetting;
    [SerializeField] private float _soundVolume = 1f;
    Dictionary<EnumAudios, AudioClip> _camera = new Dictionary<EnumAudios, AudioClip>();

    [Header("<color=green>UI Sounds</color>")]
    [SerializeField] private AudioClip _countPoints;
    public static AudioStorage Instance;
    private void Awake()
    {
        if(!Instance)
        Instance = this;
        else
            DontDestroyOnLoad(this);
       
    }
    private void Start()
    {
        LoadAudios();
    }
    public AudioClip PlayerSound(EnumAudios clip)
    {
        return _player[clip];
    }
    public AudioClip CameraSound(EnumAudios clip)
    {
        return _camera[clip];
    }
    public AudioClip GunSound(EnumAudios clip)
    {
        return _gun[clip];
    }
    public AudioClip StandardEnemySound(EnumAudios clip)
    {
        return _standardEnemy[clip];
    }
    public AudioClip SoundsGameObject(EnumAudios clip)
    {
        return _soundsGameObjects[clip];
    }
    public void LightSwitch()
    {
        AudioManager.Instance.PlaySFX(_LightSwitch, _soundVolume);
    }

    public void GlassBrokenSound()
    {
        AudioManager.Instance.PlaySFX(_glassSound, _soundVolume);
    }

    public void OpenDoorSound()
    {
        AudioManager.Instance.PlaySFX(_openDoorSound, _soundVolume);
    }

    public void CloseDoorSound()
    {
        AudioManager.Instance.PlaySFX(_closeDoorSound, _soundVolume);
    }

    public void ZapSound()
    {
        AudioManager.Instance.PlaySFX(_zapSound, _soundVolume);
    }
    public void RoombaExplosion()
    {
        AudioManager.Instance.PlaySFX(_Roombaexplosion, _soundVolume);
    }

    public void LaserAlarmSound()
    {
        AudioManager.Instance.PlaySFX(_laserAlarm, _soundVolume - 0.5f);
    }

    public void SmokeTrapSound()
    {
        AudioManager.Instance.PlaySFX(_smokeTrapSound, _soundVolume - 0.3f);
    }

    #region Camera
    public void CountPoints()
    {
        AudioManager.Instance.PlaySFX(_countPoints, _soundVolume - 0.3f);
    }
    #endregion
    private void LoadAudios()
    {
        _player.Add(EnumAudios.PlayerDash, _Dash);
        _player.Add(EnumAudios.PlayerWalk, _walking);

        _gun.Add(EnumAudios.GunShooting, _shootingSound);
        _gun.Add(EnumAudios.GunGrabbing, _grabbingSound);

        _standardEnemy.Add(EnumAudios.EnemyAlert, _enemyAlert);
        _standardEnemy.Add(EnumAudios.EnemyChecking, _enemyHmm);
        _standardEnemy.Add(EnumAudios.EnemyConfuse, _enemyConfused);
        _standardEnemy.Add(EnumAudios.EnemyHurt, _enemyHurt);

        _camera.Add(EnumAudios.CameraDetection, _cameraDetection);
        _camera.Add(EnumAudios.CameraResetting, _cameraResetting);
    } 
}
