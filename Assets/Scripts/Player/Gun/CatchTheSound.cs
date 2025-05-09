using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchTheSound : MonoBehaviour
{
    private AbstractSound _scriptSound;
    private PlayerGrabbingGun _gun;

     void Start()
    {
        _gun = GetComponentInParent<PlayerGrabbingGun>();
        Destroy(gameObject,0.25f);
    }
    void OnTriggerEnter(Collider sound)
    {
        if (sound.GetComponent<AbstractSound>()==true)
        {
            _scriptSound.SetTarget(transform, 50.0f);
        }
    }
    void OnTriggerExit(Collider sound)
    {
        if (sound.GetComponent<AbstractSound>() == true)
        {
            _scriptSound = sound.GetComponent<AbstractSound>();
            _scriptSound.SetTarget(null, 0.0f);
        }
    }
}
