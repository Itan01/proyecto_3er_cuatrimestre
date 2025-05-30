using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummonSoundFromObject : MonoBehaviour, IInteractableObject
{
    [SerializeField] private GameObject _sound;
    private Animator _animator;
    private AbstractSound _script;
    [SerializeField] private int _value;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void OnInteract()
    {
        GameManager.Instance.SetScore += _value;
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _animator.SetBool("Shine",true);
        }
    }
    private void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _animator.SetBool("Shine", false);
        }
    }
    void OnCollisionEnter(Collision Sound)
    {
        if(Sound.gameObject.GetComponent<AbstractSound>())
        {
            var Summoner = Instantiate(_sound, transform.position + new Vector3(0.0f,1.0f,0.0f), Quaternion.identity);
            _script = Summoner.GetComponent<AbstractSound>();
            _script.SetDirection(Summoner.transform.position - transform.position, 5.0f, 1.0f);
         
            Destroy(gameObject);
        }
    }

}

