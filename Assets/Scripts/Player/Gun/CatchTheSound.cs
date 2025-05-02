using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchTheSound : MonoBehaviour
{
    private AbsStandardSoundMov _scriptSound;
    private GrabbingGun _gun;

     void Start()
    {
        _gun = GetComponentInParent<GrabbingGun>();
        Destroy(gameObject,0.25f);
    }
    void OnTriggerEnter(Collider sound)
    {
        if (sound.gameObject.CompareTag("Sound"))
        {
            _scriptSound = sound.GetComponent<AbsStandardSoundMov>();
            _scriptSound.SetTarget(_gun.transform,20.0f);
        }
    }
    void OnTriggerExit(Collider sound)
    {
        if (sound.gameObject.CompareTag("Sound"))
        {
            _scriptSound = sound.GetComponent<AbsStandardSoundMov>();
            _scriptSound.SetTarget(null, 0.0f);
        }
    }
}
