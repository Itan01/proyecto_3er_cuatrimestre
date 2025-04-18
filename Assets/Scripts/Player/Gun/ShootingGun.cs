using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGun : MonoBehaviour
{
   /* private GameObject _soundShoot;
    private bool _hasASound = false;
    private SoundMovement _scriptSound;
    [SerializeField] private Transform _spawnProyectil, _orientationProyectil;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasASound && Input.GetMouseButtonDown(0))
        {
            ThrowSound();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            _hasASound = true;
        }
    }
    private void ThrowSound()
    {
        var ThrowingSound = Instantiate(_soundShoot, _spawnProyectil.position, Quaternion.identity);
        _scriptSound = ThrowingSound.GetComponent<SoundMovement>();
        _scriptSound.SetVector(_orientationProyectil.position, 1, "sonido1");
        _hasASound = false;

    }*/
}
