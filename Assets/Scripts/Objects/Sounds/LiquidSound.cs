using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSound : AbsStandardSoundMov
{
    protected override void Start()
    {
        base.Start();
        _rb.useGravity = true;
        _rb.freezeRotation = true;
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        Move();
    }
}
