using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSound : AbsStandardSoundMov
{
    [SerializeField] private float _forceGravity = 1.0f;
    protected override void Start()
    {
        base.Start();
        _rb.mass = _forceGravity;
        _rb.useGravity = true;
        _rb.freezeRotation = true;
        _index = 2;
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
