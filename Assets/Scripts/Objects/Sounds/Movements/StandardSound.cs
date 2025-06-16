using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class StandardSound : AbstractSound
{
    private ParticlesManager _particleManager;
    [SerializeField] private GameObject _soundExplosion;
    protected override void Start()
    {
        base.Start();
        if (_playerShooted)
        {
            _particleManager = GetComponentInChildren<ParticlesManager>();
            _particleManager.StartLoop();
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
        if (_playerShooted && !_canCatch)
        {
            SummonExplosion();
        }
        Destroy(gameObject,0.15f);
    }
    protected void SummonExplosion()
    {
        Instantiate(_soundExplosion,transform.position, Quaternion.identity);
    }
}
