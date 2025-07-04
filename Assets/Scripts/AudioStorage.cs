using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStorage : MonoBehaviour
{
    [SerializeField] private AudioClip _crashSound, _shootingSound, _grabbingSound, _glassSound, _smokeTrapSound, _enemyConfusedSound, _enemyTensionSound, _laserAlarm;
    [SerializeField] private AudioClip _openDoorSound, _closeDoorSound, _zapSound;
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
}
