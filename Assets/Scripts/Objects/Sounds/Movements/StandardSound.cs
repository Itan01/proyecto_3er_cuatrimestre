using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardSound : AbstractSound
{
    [SerializeField] private ParticleSystem[] _lines;
    [SerializeField] private GameObject _soundExplosion;
    protected override void Start()
    {
        base.Start();
        if (_playerSummoned)
        {
            for (int i = 0; i < _lines.Length; i++)
                _lines[i].Play();
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
        if (_playerSummoned)
        {
            SummonExplosion();
        }
        Destroy(gameObject, 0.1f);
    }
    protected void SummonExplosion()
    {
        Instantiate(_soundExplosion,transform.position, Quaternion.identity);
    }
}
