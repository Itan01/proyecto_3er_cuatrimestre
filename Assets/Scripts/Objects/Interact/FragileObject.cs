using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileObject : AbstractObjects, ISoundInteractions
{
    [SerializeField] private GameObject _sound;
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
    public float GetSize()
    {
        return _sizeMultiplier;
    }

    protected override void SetFeedback(bool State)
    {
        if (_destroyed) return;
        _animator.SetBool("Shine", State);
    }
    public void IIteraction(bool PlayerShootIt)
    {
        var Sound = Instantiate(_sound, transform.position, Quaternion.identity);
        Sound.GetComponent<SoundRadiusTrigger>().SetMultiplier(GetSize());
        AudioStorage.Instance.GlassBrokenSound();
        DesactivateObject();
        _destroyed=true;
    }   

}

