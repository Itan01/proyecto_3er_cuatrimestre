using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    private AbstracDoors _doorManager;
    [SerializeField]private ParticleSystem[] _particles;
    [SerializeField] private AudioClip _buttonSound;
    private float _soundVolume = 1.0f;
    void Start()
    {
        _doorManager = GetComponentInParent<AbstracDoors>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AbstractSound script))
        {
            if(script.PlayerShootIt())
            _doorManager.CheckStatus();
            if (_buttonSound != null)
            {
                AudioSource.PlayClipAtPoint(_buttonSound, transform.position, _soundVolume);
            }
        }
    }
}
