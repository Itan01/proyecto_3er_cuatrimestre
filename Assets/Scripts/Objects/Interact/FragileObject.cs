using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FragileObject : AbstractObjects
{
    [SerializeField] private GameObject _sound;
    private ParticleSystem _particles;

    protected override void Start()
    {
        base.Start();
        _particles = GetComponentInChildren<ParticleSystem>();
    }
    void OnCollisionEnter(Collision Sound)
    {
        if(Sound.gameObject.GetComponent<AbstractSound>())
        {
            var Summoner = Instantiate(_sound, transform.position + new Vector3(0.0f,1.0f,0.0f), Quaternion.identity);
             AbstractSound script = Summoner.GetComponent<AbstractSound>();
            script.SetDirection(Summoner.transform.position - transform.position, 5.0f, 1.0f);
         
            Destroy(gameObject);
        }
    }
    protected override void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _animator.SetBool("Shine", true);
            //_particles.Play();
        }
    }
    protected override void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _animator.SetBool("Shine", false);
            //  _particles.Stop();
        }
    }

}

