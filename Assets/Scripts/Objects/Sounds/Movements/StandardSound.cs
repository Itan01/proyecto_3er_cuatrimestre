using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardSound : AbstractSound
{
    [SerializeField] private ParticlesSoundManager _particleManager;
    [SerializeField] private GameObject _soundExplosion;
    protected override void Start()
    {
        base.Start();
        if (_playerSummoned)
        {
            _particleManager = GetComponentInChildren<ParticlesSoundManager>();
            _particleManager.StarPlay();
        }
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
        if (_playerShooted)
        {
            SummonExplosion();
        }
        Destroy(gameObject);
    }
    protected void SummonExplosion()
    {
        Instantiate(_soundExplosion,transform.position, Quaternion.identity);
    }
}
