using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Factory_Explosion_Crash_Sound))]
public class Factory_CrashSound : Factory<Sound_Crash>
{
    public static Factory_CrashSound Instance;
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
    public override Sound_Crash Create()
    {
        var x = _pool.Get();
        x.Initialize(_pool);
        return x;
    }
    protected override Sound_Crash CreatePrefab()
    {
        var x = Instantiate(_prefab);
        return x;
    }
    protected override void Activate(Sound_Crash obj)
    {
        obj.gameObject.SetActive(true);
    }

    protected override void Desactivate(Sound_Crash obj)
    {
        obj.gameObject.SetActive(false);
    }
}
