using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrabbingGun : MonoBehaviour
{
    private bool _hasASound = false;
    public bool _catchingSound = false;
    [SerializeField] private GameObject _AreaCathing;
    [SerializeField] private Transform _spawnProyectil,_orientationProyectil;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (!_catchingSound && Input.GetMouseButtonDown(1))
        {
            CatchSound();
        }
        if (_catchingSound && Input.GetMouseButtonUp(1))
        {
            _catchingSound = false;
        }
    }

    private void CatchSound()
    {
        _catchingSound = true;
        var _Area = Instantiate(_AreaCathing, _spawnProyectil.position, Quaternion.identity, _spawnProyectil);
        _Area.transform.LookAt(_orientationProyectil);
    }

    public void CheckSound(bool Checker)
    {
        _hasASound = Checker;
    }
}
