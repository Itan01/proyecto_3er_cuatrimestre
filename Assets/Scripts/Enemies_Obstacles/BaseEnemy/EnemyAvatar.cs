using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvatar : MonoBehaviour
{

    [SerializeField]private AudioClip _walking;
    private AudioSource _source;

    private void Start()
    {
        if(_source==null) _source = GetComponentInParent<AudioSource>();
    }
    public void PlayAudioWalk()
    {
       _source.pitch= Random.Range(0.95f, 1.05f);
       if(_walking !=null) _source.PlayOneShot(_walking,0.1f);
    }
}
