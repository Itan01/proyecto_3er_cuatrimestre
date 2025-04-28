using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSound : AbsStandardSoundMov
{
    protected override void Start()
    {
        base.Start();
        _rb.freezeRotation = true;
        _index = 2;
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
