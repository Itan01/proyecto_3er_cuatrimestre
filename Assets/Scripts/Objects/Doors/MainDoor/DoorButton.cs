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
    private void OnTriggerEnter(Collider Sound)
    {
        if (Sound.TryGetComponent<AbstractSound>(out AbstractSound ScriptSound))
        {
            _doorManager.CheckStatus();
            for (int i = 0; i < _particles.Length; i++)
                _particles[i].Play();
            Destroy(ScriptSound.gameObject);
        }
    }

    public void OnInteract()
    {
        Debug.Log("Wrong VoiceCode");
    }
}
