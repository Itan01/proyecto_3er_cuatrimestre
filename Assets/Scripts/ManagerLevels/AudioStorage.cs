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
    [Header("<color=green>Objects</color>")]
    [SerializeField] private AudioClip _objFragPlayerNear;
    [SerializeField] private AudioClip _objValuablePlayerNear;
    [SerializeField] private AudioClip _destroyValuableObj;
    [SerializeField] private AudioClip _destoyObjFrag;
    [SerializeField] private AudioClip _grabObj;
    [SerializeField] private AudioClip _fuseBoxGeneratesSounds;
    Dictionary<EAudios, AudioClip> _objects = new Dictionary<EAudios, AudioClip>();

    [Header("<color=green>Rooms</color>")]

    [SerializeField] private AudioClip _glassSound;
    [SerializeField] private AudioClip _smokeTrapSound;
    [SerializeField] private AudioClip _laserAlarm;
    Dictionary<EAudios, AudioClip> _rooms = new Dictionary<EAudios, AudioClip>();

    [Header("<color=green>Player Sounds</color>")]

    [SerializeField] private AudioClip _Dash;
    [SerializeField] private AudioClip _walking; 
    [SerializeField] private AudioClip _cadenzaDead;
    Dictionary<EAudios, AudioClip> _player = new Dictionary<EAudios, AudioClip>();

    [Header("<color=green>PlayerGun Sounds</color>")]
    [SerializeField] private AudioClip _shootingSound;
    [SerializeField] private AudioClip _grabbingSound;
    [SerializeField] private AudioClip _crashSound;
    [SerializeField] private AudioClip _soundPath;
    Dictionary<EAudios, AudioClip> _gun = new Dictionary<EAudios, AudioClip>();

    [Header("<color=green>Lights Sound </color>")]
    [SerializeField] private AudioClip _LightSwitch;

    [Header("<color=green>Roomba Sounds</color>")]
    [SerializeField] private AudioClip _Roombaexplosion;
    [SerializeField] private AudioClip _spawnRoomba;
    [SerializeField] private AudioClip _walkRoomba;
    Dictionary<EAudios, AudioClip> _roomba = new Dictionary<EAudios, AudioClip>();

    [Header("<color=green>Enemy Sounds</color>")]
    [SerializeField] private AudioClip _enemyConfused;
    [SerializeField] private AudioClip _enemyAlert;
    [SerializeField] private AudioClip _enemyHmm;
    [SerializeField] private AudioClip _enemyHurt;
    [SerializeField] private AudioClip _enemyWalk;
    [SerializeField] private AudioClip _stunnedEnemy;
     Dictionary<EAudios, AudioClip> _standardEnemy = new Dictionary<EAudios, AudioClip>();

    [Header("<color=green>Door Sounds</color>")]
    [SerializeField] private AudioClip _openDoorSound;
    [SerializeField] private AudioClip _closeDoorSound;

    [Header("<color=green>Camera Sounds</color>")]
    [SerializeField] private AudioClip _cameraDetection;
    [SerializeField] private AudioClip _cameraResetting;
    [SerializeField] private float _soundVolume = 1f;
    Dictionary<EAudios, AudioClip> _camera = new Dictionary<EAudios, AudioClip>();

    [Header("<color=green>UI Sounds</color>")]
    [SerializeField] private AudioClip _countPoints;
    [SerializeField] private AudioClip _uiGun;
    [SerializeField] private AudioClip _timer;
    Dictionary<EAudios, AudioClip> _ui = new Dictionary<EAudios, AudioClip>();

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
    public AudioClip PlayerSound(EAudios clip)
    {
        return _player[clip];
    }
    public AudioClip CameraSound(EAudios clip)
    {
        return _camera[clip];
    }
    public AudioClip GunSound(EAudios clip)
    {
        return _gun[clip];
    }
    public AudioClip StandardEnemySound(EAudios clip)
    {
        return _standardEnemy[clip];
    }
    public AudioClip RoombaSound(EAudios clip)
    {
        return _roomba[clip];
    }
    public AudioClip SoundsGameObject(EAudios clip)
    {
        return _objects[clip];
    }
    public AudioClip UiSound(EAudios clip)
    {
        return _ui[clip];
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
        _player.Add(EAudios.PlayerDash, _Dash);
        _player.Add(EAudios.PlayerWalk, _walking);
        _player.Add(EAudios.PlayerDeath, _cadenzaDead);

        //AudioStorage.Intance.(Tipo)Player(EnumAudios.PlayerDead);

        _gun.Add(EAudios.GunShooting, _shootingSound);
        _gun.Add(EAudios.GunGrabbing, _grabbingSound);

        _standardEnemy.Add(EAudios.EnemyAlert, _enemyAlert);
        _standardEnemy.Add(EAudios.EnemyChecking, _enemyHmm);
        _standardEnemy.Add(EAudios.EnemyConfuse, _enemyConfused);
        _standardEnemy.Add(EAudios.EnemyHurt, _enemyHurt);
        _standardEnemy.Add(EAudios.EnemyWalk, _enemyWalk);

        _camera.Add(EAudios.CameraDetection, _cameraDetection);
        _camera.Add(EAudios.CameraResetting, _cameraResetting);

        _roomba.Add(EAudios.RoombaWalk, _walkRoomba);
        _roomba.Add(EAudios.RoombaExplosion,_Roombaexplosion);
        _roomba.Add(EAudios.RoombaSpawn,_spawnRoomba);

        _ui.Add(EAudios.Timer, _timer);
        _objects.Add(EAudios.GrabObject, _grabObj);
        _objects.Add(EAudios.FuseBox,_fuseBoxGeneratesSounds);
    } 
}
