using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    private AbstracDoors _doorManager;
    [SerializeField]private ParticleSystem[] _particles;
    void Start()
    {
        _doorManager = GetComponentInParent<AbstracDoors>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AbstractSound>())
        {
            _doorManager.CheckStatus();
        }
    }
}
