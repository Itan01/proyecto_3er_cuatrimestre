using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : AbsStandardSoundMov
{
    protected override void Start()
    {
        base.Start();
        _rb.freezeRotation = true;
        _index = 3;
    }
    protected override void Update()
    {
        base.Update();
        TravelSize();
    }
    protected override void FixedUpdate()
    {
        Move();
    }
}
