using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructExplosion : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}

