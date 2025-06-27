using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Rendering;

public class StandardSound : AbstractSound
{
    private ParticlesManager _particleManager;
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
            if (_crashSound != null)
            {
                AudioSource.PlayClipAtPoint(_crashSound, transform.position, _soundVolume);
            }
        }

        Destroy(gameObject,0.25f);
    }

    protected void SummonExplosion()
    {
        Instantiate(_soundExplosion,transform.position, Quaternion.identity);
    }
}
