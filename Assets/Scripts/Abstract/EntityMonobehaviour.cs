using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMonobehaviour : MonoBehaviour
{
    protected Rigidbody _rb;
    protected Animator _animator;
    protected CapsuleCollider _capsuleCollider;

    protected virtual void Start()
    {
        GetComponents();
        GetScripts();
    }
    protected virtual void Update()
    {
        
    }
    protected virtual void FixedUpdate()
    {

    }
    protected virtual void GetComponents()
    {
        _rb =GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _animator =GetComponentInChildren<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }
    protected virtual void GetScripts()
    {

    }

}
