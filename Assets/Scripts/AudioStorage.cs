using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStorage : MonoBehaviour
{
    [SerializeField] private AudioClip _glassSound, _smokeTrapSound, _laserAlarm, _zapSound;
    [Header("<color=green>Player Sounds</color>")]
    [SerializeField] private AudioClip _crashSound;
    [SerializeField] private AudioClip _shootingSound;
    [SerializeField] private AudioClip _grabbingSound;
    [Header("<color=green>Roomba Sounds</color>")]

    [Header("<color=green>Enemy Sounds</color>")]
    [SerializeField] private AudioClip _enemyConfusedSound;
    [SerializeField] private AudioClip _enemyTensionSound;
    [Header("<color=green>Door Sounds</color>")]
    [SerializeField] private AudioClip _openDoorSound;
    [SerializeField] private AudioClip _closeDoorSound;
    [Header("<color=green>Camera Sounds</color>")]
    [SerializeField] private AudioClip _cameraDetection;
    [SerializeField] private AudioClip _cameraResetting;
    [SerializeField] private float _soundVolume = 1f;
    public static AudioStorage Instance;
    public AudioClip EnemySound()
    {

        return _enemyConfusedSound;
    }


    private void Awake()
    {
        Instance = this;

    }
    public void CrashSound()
    {
        AudioManager.Instance.PlaySFX(_crashSound, _soundVolume);
    }
    public void ShootingSound()
    {
        AudioManager.Instance.PlaySFX(_shootingSound, _soundVolume);
    }
    public void GrabbingSound()
    {
        AudioManager.Instance.PlaySFX(_grabbingSound, _soundVolume);
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
    public void EnemyConfusedSound()
    {
        AudioManager.Instance.PlaySFX(_enemyConfusedSound, _soundVolume - 0.5f);
    }
    public void EnemyTensionSound()
    {
        AudioManager.Instance.PlaySFX(_enemyTensionSound, _soundVolume);
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
    public void CameraDectection()
    {
        AudioManager.Instance.PlaySFX(_cameraDetection, _soundVolume - 0.3f);
    }
    public void CameraResetting()
    {
        AudioManager.Instance.PlaySFX(_cameraResetting, _soundVolume - 0.3f);
    }
    #endregion
}
