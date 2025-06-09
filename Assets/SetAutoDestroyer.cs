using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAutoDestroyer : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy=1.0f; 
    void Start()
    {
        Destroy(gameObject, _timeToDestroy);
    }
}
