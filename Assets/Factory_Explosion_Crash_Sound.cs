using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_Explosion_Crash_Sound : Factory<Sound_Crash_Radius>
{
    public static Factory_Explosion_Crash_Sound Instance;
    protected override void Awake()
    {
        base.Awake();
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public override Sound_Crash_Radius Create()
    {
        var x = _pool.Get();
        x.Initialize(_pool);
        return x;
    }

    protected override void Activate(Sound_Crash_Radius obj)
    {
        obj.gameObject.SetActive(true);
    }

    protected override Sound_Crash_Radius CreatePrefab()
    {
        var x = Instantiate(_prefab);
        return x;
    }

    protected override void Desactivate(Sound_Crash_Radius obj)
    {
        obj.gameObject.SetActive(false);
    }
}