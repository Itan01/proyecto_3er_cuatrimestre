using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_CrashSound : Factory<Sound_Crash>
{
    protected override void Awake()
    {
        base.Awake();
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
