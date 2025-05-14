using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSound : AbstractSound
{
    private BounceObject _scriptBounce;
    private bool _isOnGround=false;
    [SerializeField] private LayerMask _groundRayMask;
    [SerializeField] private float _timerBetweenBounce;

    protected override void Start()
    {
        base.Start();
        GetScript();
        _rb.useGravity = true;
        _index = 2;
    }
    protected override void Update()
    {
        base.Update();
        CheckBounce();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Bounce();
    }

    private void CheckBounce()
    {
        _isOnGround = _scriptBounce.CheckIfOnGround(_size);
        _timerBetweenBounce += 5 * Time.deltaTime;
    }
    private void Bounce()
    {
        if (_isOnGround)
        {
            _scriptBounce.MakeBounce(_timerBetweenBounce);
            _isOnGround = false;
            _timerBetweenBounce = 0.0f;
        }
    }

    private void GetScript()
    {
        _scriptBounce = new BounceObject(_groundRayMask, transform, _rb);
    }
}
