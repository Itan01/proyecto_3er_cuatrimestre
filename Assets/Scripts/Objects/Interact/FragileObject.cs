using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileObject : AbstractObjects, ISoundInteractions
{
    [SerializeField] private float _sizeMultiplier;
    private bool _destroyed=false;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {

        base.Update();
    }

    protected override void SetFeedback(bool State)
    {
        if (_destroyed) return;
        _animator.SetBool("Shine", State);
    }
    public void IIteraction(bool PlayerShootIt)
    {
        var Sound = Factory_Explosion_Crash_Sound.Instance.Create();
        Sound.GetComponent<Sound_Crash_Radius>().SetMultiplier(_sizeMultiplier);
        AudioStorage.Instance.GlassBrokenSound();
        DesactivateObject();
        _destroyed=true;
    }   

}

