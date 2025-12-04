using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Sound_Crash_Radius : MonoBehaviour
{
    private float _size=1, _multiplier=5, _timer;
    private ObjectPool<Sound_Crash_Radius> _pool;
    private void Update()
    {
        if (_timer > 1) Refresh();
        transform.localScale = new Vector3(_size, _size, _size);
        _size += Time.deltaTime * _multiplier;
        _timer+= Time.deltaTime;
    }
    public void Initialize(ObjectPool<Sound_Crash_Radius> Pool)
    {
        _pool = Pool;
       

    }
    private void Refresh()
    {
        _multiplier = 5.0f;
        _size = 1.0f;
        _timer = 0.0f;
        _pool.Release(this);
    }
    public void Alert()
    {
        EventManager.Trigger(EEvents.DetectPlayer, transform);

    }
    //protected void OnTriggerEnter(Collider Entity)
    //{
    //    if (Entity.TryGetComponent(out AbstractEnemy Script))// sigue el sonido, si el enemigo persigue al jugador, ignora el sonido
    //    {
    //        if (!Script.GetActivate()) return;
    //        if(Script.GetMode()!= 3  && Script.GetMode() != 1 && Script.GetMode() != 6)
    //        {
    //            Script.SetPosition(transform.position);
    //            Script.SetModeByIndex(5);
    //        }
    //    }
    //}

    public void SetMultiplier(float Multiplier)
    {
        if (Multiplier == 0)
            Multiplier = 1;
        _multiplier = Multiplier;
    }
}

