using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab_and_shoot : MonoBehaviour
{
    [SerializeField] private bool _hasASound = false;
    [SerializeField] public bool _catchingSound = false;
    private float _sizeBullet = 1.0f;
    [SerializeField] private GameObject _soundShooot;
    private SoundMov _scriptSound;
    [SerializeField] private GameObject _AreaReference;
    [SerializeField] private GameObject _spawnArea;
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
        if (!_catchingSound && Input.GetMouseButtonDown(1))
        {
            StartToCatchSound();
        }
        if (_catchingSound && Input.GetMouseButtonUp(1))
        {
            _catchingSound = false;
        }
    }

    private void ThrowSound()
    {
        var ThrowingSound = Instantiate(_soundShooot, _spawnProyectil.position, Quaternion.identity);
        _scriptSound = ThrowingSound.GetComponent<SoundMov>();
        _scriptSound.SetVector(_orientationProyectil.position, _sizeBullet, "sonido1");
        _hasASound = false;

    }

    private void StartToCatchSound()
    {
        _catchingSound = true;
        var _Area = Instantiate(_AreaReference, _spawnProyectil.position, Quaternion.identity, _spawnProyectil);
        _Area.transform.LookAt(_orientationProyectil);
    }
    public void CatchSound(float Charge)
    {
        _hasASound = true;
        _sizeBullet = Charge;
    }
}
