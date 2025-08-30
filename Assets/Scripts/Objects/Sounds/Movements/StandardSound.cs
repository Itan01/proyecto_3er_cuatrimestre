using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Rendering;

public class StandardSound : AbstractSound
{
    private ParticlesManager _particleManager;
    private bool _explosion=false;
    [SerializeField] private GameObject _soundExplosion;
    [SerializeField] private AudioClip _crashSound;
    private float _soundVolume = 1.0f;

    protected override void Start()
    {
        base.Start();
        _particleManager = GetComponentInChildren<ParticlesManager>();
        if (_playerShooted)
            _particleManager.PlayOnce();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    private void OnCollisionEnter(Collision Enviroment)
    {
        if (_playerShooted && !_canCatch)
        {
            SummonExplosion();
            AudioManager.Instance.PlaySFX(_crashSound, _soundVolume);
        }

        Destroy(gameObject,0.25f);
    }

    protected void SummonExplosion()
    {
        if (_explosion) return;
        _explosion = true;
        Instantiate(_soundExplosion,transform.position, Quaternion.identity);
    }
}
