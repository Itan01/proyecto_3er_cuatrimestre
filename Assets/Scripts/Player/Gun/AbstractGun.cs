using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGun : MonoBehaviour
{
    protected bool _hasASound = false;
    protected float _timer = 0.0f, _timerRef = 1.0f;
    protected Transform _spawnProyectil;
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
