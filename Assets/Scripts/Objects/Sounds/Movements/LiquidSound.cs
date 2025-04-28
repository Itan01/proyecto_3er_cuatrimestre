using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSound : AbsStandardSoundMov
{
    private BounceObject _scriptBounce;
    private bool _isOnGround=false;
    [SerializeField] private float _timerBetweenBounce;

    protected override void Start()
    {
        base.Start();
        _rb.useGravity = true;
        _rb.freezeRotation = true;
        _index = 1;
        _scriptBounce = GetComponent<BounceObject>();
    }
    protected override void Update()
    {
        base.Update();
        CheckBounce();
    }
    protected override void FixedUpdate()
    {
        Move();
        Bounce();
    }

    private void CheckBounce()
    {
        _isOnGround = _scriptBounce.CheckIfOnGround();
        _timerBetweenBounce += 1 * Time.deltaTime;
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
}
