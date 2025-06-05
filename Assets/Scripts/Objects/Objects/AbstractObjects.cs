using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractObjects : MonoBehaviour
{
    protected Animator _animator;
    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected virtual void OnTriggerEnter(Collider Player)
    {
    }
    protected virtual void OnTriggerExit(Collider Player)
    {
    }
}
