using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DoorButton : MonoBehaviour, IInteractableObject
{
    private MainDoorManager _doorManager;
    [SerializeField]private ParticleSystem[] _particles;
    void Start()
    {
        _doorManager = GetComponentInParent<MainDoorManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 28)
        {
            _doorManager.CheckStatus();
            for (int i = 0; i < _particles.Length; i++)
                _particles[i].Play();
        }
    }

    public void OnInteract()
    {
        Debug.Log("Wrong VoiceCode");
    }
}
