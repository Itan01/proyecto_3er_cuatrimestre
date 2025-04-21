using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsSettingGun : MonoBehaviour
{
    [SerializeField] protected bool _hasASound = false;
    [SerializeField] protected float _timer = 0.0f, _timerRef = 0.25f;
    [SerializeField] protected Transform _spawnProyectil, _orientationProyectil;
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }
    public virtual void CheckSound(bool Checker)
    {
        _hasASound = Checker;
    }
}
